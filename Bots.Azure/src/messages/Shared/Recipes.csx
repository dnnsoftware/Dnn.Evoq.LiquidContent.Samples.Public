using Newtonsoft.Json;

[Serializable]
public class RecipesCollection
{
    [JsonProperty(PropertyName = "documents")]
    public List<Recipe> Recipes { get; set; }
}

[Serializable]
public class RecipeTypesCollection
{
    [JsonProperty(PropertyName = "documents")]
    public List<RecipeType> RecipeTypes { get; set; }
}

[Serializable]
public class RecipeType
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
}


    [Serializable]
public class Recipe 
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    [JsonProperty(PropertyName = "details")]
    public RecipeDetails Details { get; set; }

    [JsonProperty(PropertyName = "updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty(PropertyName = "updatedBy")]
    public RecipeAuthor Author { get; set; }
}

[Serializable]
public class RecipeDetails
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "ingredients")]
    public string Ingredients { get; set; }

    [JsonProperty(PropertyName = "instructions")]
    public string Instructions { get; set; }

    //[JsonProperty(PropertyName = "recipeType")]
    //public string RecipeType { get; set; }

    [JsonProperty(PropertyName = "pictureUrl")]
    public string ImageUrl { get; set; }
}

[Serializable]
public class RecipeAuthor
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "thumbnail")]
    public string ProfilePictureUrl { get; set; }

}
