using System;
using Microsoft.Bot.Builder.FormFlow;

public enum RecipeTypeEnum { Starters = 1, Breakfast, Salads, Soups, MainCourses, Sides, Desserts, All };

// For more information about this template visit http://aka.ms/azurebots-csharp-form
[Serializable]
public class RecipeForm
{
    [Prompt("Which {&} do you have?")]
    public string Ingredients { get; set; }

    [Prompt("Please select the type of recype you want to cook {||}")]
    public RecipeTypeEnum RecypeType { get; set; }

    public static IForm<RecipeForm> BuildForm()
    {
        // Builds an IForm<T> based on BasicForm
        return new FormBuilder<RecipeForm>().Build();
    }

    public static IFormDialog<RecipeForm> BuildFormDialog(FormOptions options = FormOptions.PromptInStart)
    {
        // Generated a new FormDialog<T> based on IForm<BasicForm>
        return FormDialog.FromForm(BuildForm, options);
    }
}
