#load ".\Shared\HeroCardExtensions.csx"
#load ".\Shared\Recipes.csx"
#load "RecipeForm.csx"

using System;
using System.Configuration;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;


// For more information about this template visit http://aka.ms/azurebots-csharp-basic
[Serializable]
public class RootDialog : IDialog<object>
{
    private const string RootDialog_Welcome_Ingredients = "Type ingredients";
    private const string RootDialog_Welcome_Ingredients_Picture = "Upload pic of ingredients";
    private const string RootDialog_Welcome_Support = "Call DNN Support";
    private const string RootDialog_Welcome_Error = "That is not a valid option. Please try again.";
    private const string RootDialog_Support_Message = "Support will contact you shortly. Have a nice day :)";

    private List<Recipe> Recipes { get; set; }
    private RecipeType RecipeType { get; set; }

    private static string APIEndpoint => ConfigurationManager.AppSettings["EvoqAPIEndpoint"];
    private static string APIKey => ConfigurationManager.AppSettings["EvoqAPIKey"];
    
    public Task StartAsync(IDialogContext context)
    {
        try
        {
            context.Wait(MessageReceivedAsync);
        }
        catch (OperationCanceledException error)
        {
            return Task.FromCanceled(error.CancellationToken);
        }
        catch (Exception error)
        {
            return Task.FromException(error);
        }

        return Task.CompletedTask;
    }

    public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
    {
        var message = await argument;
        await this.WelcomeMessageAsync(context);
    }

    private async Task WelcomeMessageAsync(IDialogContext context)
    {
        var reply = context.MakeMessage();

        var options = new[]
        {
                RootDialog_Welcome_Ingredients,
                RootDialog_Welcome_Ingredients_Picture,
                RootDialog_Welcome_Support
            };

        HeroCardExtensions.AddHeroCard(ref reply,
            "Recipes Bot",
            "Your agent that allows you to cook Liquid Content recipes",
            options, 
            new[] { "https://raw.githubusercontent.com/davidjrh/dnn.recipes.bot/master/dnnrecipes.jpg" });

        await context.PostAsync(reply);

        context.Wait(this.OnOptionSelected);
    }

    private async Task OnOptionSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
    {
        var message = await result;

        if (message.Text == RootDialog_Welcome_Ingredients)
        {
            context.Call(RecipeForm.BuildFormDialog(FormOptions.PromptInStart), FormComplete);
        }
        else if (message.Text == RootDialog_Welcome_Ingredients_Picture)
        {
            await context.PostAsync("This will be on the second tutorial using **Cognitive Services**. Come back soon!");
            context.Wait(MessageReceivedAsync);
        }
        else if (message.Text == RootDialog_Welcome_Support)
        {
            await context.PostAsync("Support will contact you shortly. Have a nice day :)");
            context.Wait(MessageReceivedAsync);
        }
        else
        {
            await this.StartOverAsync(context, RootDialog_Welcome_Error);
        }
    }

    private async Task FormComplete(IDialogContext context, IAwaitable<RecipeForm> result)
    {
        try
        {
            var form = await result;
            if (form != null)
            {
                await context.PostAsync("Ok. Give me a second while I look for something tasty...");
                if (this.RecipeType == null)
                {
                    this.RecipeType = await GetRecipeTypeAsync();
                }
                this.Recipes = await GetRecipesAsync(form.Ingredients, this.RecipeType);
                if (this.Recipes != null && this.Recipes.Count > 0)
                {
                    await this.ShowRecipesAsync(context);
                }
                else
                {
                    await context.PostAsync("Sorry, seems there are no recipes in my library for those ingredients! Do you want me to order a pizza?");
                    context.Wait(MessageReceivedAsync);
                }                
            }
            else
            {
                await context.PostAsync("Form returned empty response! Type anything to restart it.");
                context.Wait(MessageReceivedAsync);
            }
        }
        catch (OperationCanceledException)
        {
            await context.PostAsync("You canceled the form! Type anything to restart it.");
            context.Wait(MessageReceivedAsync);
        }

        
    }

    protected async Task ShowRecipesAsync(IDialogContext context)
    {
        await context.PostAsync($"Found {this.Recipes.Count} recipes:");
        var carouselCards = this.Recipes.Select(it => new HeroCard
        {
            Title = it.Name,
            Text = it.Description,
            Images = new List<CardImage> {
                new CardImage(string.IsNullOrEmpty(it.Details.ImageUrl) ? 
                    "https://raw.githubusercontent.com/davidjrh/dnn.recipes.bot/master/no-image-icon.png" : 
                    it.Details.ImageUrl, it.Name)
            },
            Buttons = new List<CardAction>
                        {
                            new CardAction(ActionTypes.ImBack, "Select", value: it.Name)
                        }
        });

        var reply = context.MakeMessage();
        reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        reply.Attachments = new List<Attachment>();
        foreach (var card in carouselCards)
        {
            reply.Attachments.Add(card.ToAttachment());
        }
        await context.PostAsync(reply);
        context.Wait(this.OnRecipeSelected);
    }

    private async Task OnRecipeSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
    {
        var message = await result;

        var recipe = this.Recipes.FirstOrDefault(x => x.Name == message.Text);
        if (recipe != null)
        {
            await context.PostAsync($"**{recipe.Name.ToUpperInvariant()}**");
            await context.PostAsync(recipe.Description);
            await context.PostAsync($"**Ingredients**: {recipe.Details.Ingredients}");
            await context.PostAsync($"**Instructions**: {recipe.Details.Instructions}");
        }
        else
        {
            await this.StartOverAsync(context, "That recipe is not an option");
        }
    }


    static public async Task<List<Recipe>> GetRecipesAsync(string ingredients, RecipeType recipeType)
    {
        if (recipeType == null) return null;
        var parsedIngredients = ParseIngredients(ingredients);
        var request = (HttpWebRequest)WebRequest.Create(
          $"{APIEndpoint}/content/api/contentitems?contenttypeid={recipeType.Id}&searchtext={WebUtility.UrlEncode(parsedIngredients)}");
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Headers = new WebHeaderCollection();
        request.Headers.Add("Authorization", $"Bearer {APIKey}");
        using (var response = await request.GetResponseAsync() as HttpWebResponse)
        {
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Server error (HTTP {response.StatusCode}: {response.StatusDescription}).");
            using (Stream stream = response.GetResponseStream())
            using (StreamReader streamReader = new StreamReader(stream))
            {
                var strsb = await streamReader.ReadToEndAsync();
                var items = Newtonsoft.Json.JsonConvert.DeserializeObject<RecipesCollection>(strsb);
                if (items != null && items.Recipes != null)
                    return items.Recipes;
                return null;
            }
        }
    }

    static public async Task<RecipeType> GetRecipeTypeAsync()
    {
        var request = (HttpWebRequest)WebRequest.Create(
          $"{APIEndpoint}/content/api/contenttypes?searchtext=recipe");
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Headers = new WebHeaderCollection();
        request.Headers.Add("Authorization", $"Bearer {APIKey}");
        using (var response = await request.GetResponseAsync() as HttpWebResponse)
        {
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Server error (HTTP {response.StatusCode}: {response.StatusDescription}).");
            using (Stream stream = response.GetResponseStream())
            using (StreamReader streamReader = new StreamReader(stream))
            {
                var strsb = await streamReader.ReadToEndAsync();
                var items = Newtonsoft.Json.JsonConvert.DeserializeObject<RecipeTypesCollection>(strsb);
                if (items != null && items.RecipeTypes != null)
                    return items.RecipeTypes.FirstOrDefault();
                return null;
            }
        }
    }


    private static string ParseIngredients(string ingredients)
    {
        var ignoreList = new string[] { "and", "or" };
        var list = ingredients.Trim().Replace(".", ",").Replace(" ", ",").Split(',');
        return String.Join("+", list.Where(x => !string.IsNullOrEmpty(x) && !ignoreList.Contains(x)));
    }


    private async Task StartOverAsync(IDialogContext context, string text)
    {
        var message = context.MakeMessage();
        message.Text = text;
        await this.StartOverAsync(context, message);
    }

    private async Task StartOverAsync(IDialogContext context, IMessageActivity message)
    {
        await context.PostAsync(message);
        //this.order = new Models.Order();
        await this.WelcomeMessageAsync(context);
    }

}