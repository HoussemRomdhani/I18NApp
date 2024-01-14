namespace Models;

public class Recipe
{
    public long Id { get;}
    public string Title { get;}
    public string Desctiption { get; } 
    public List<Ingredient> Ingredients { get; private set; }
    public List<Step> Steps { get; private set; }

    public Recipe(long id, string title, string decription)
    {
        Id = id;
        Title = title;
        Desctiption = decription;
        Ingredients = new List<Ingredient>();
        Steps = new List<Step>();
    }

    public void AddIngredients(IList<Ingredient> ingredients) => Ingredients.AddRange(ingredients);

    public void AddSteps(IList<Step> steps) => Steps.AddRange(steps);
}
