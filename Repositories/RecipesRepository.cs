using Dapper;
using Microsoft.Data.Sqlite;
using Models;
using Repositories.Models;
using System.Globalization;

namespace Repositories;

public class RecipesRepository
{
    private readonly DatabaseConfig databaseConfig;
    public RecipesRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }
    public async Task<IList<Recipe>> GetRecipes()
    {
        List<Recipe> result = new();

        var recipesI18N = await GetRecipesI18N();

        foreach (var item in recipesI18N)
        {
            var ingredients = await GetIngredients(item.Id);

            var steps = await GetSteps(item.Id);

            Recipe recipe = new(item.Id, item.Title, item.Description);

            recipe.AddIngredients(ingredients);
           
            recipe.AddSteps(steps);

            result.Add(recipe);
        }

        return result;
    }

    private async Task<IList<RecipeI18N>> GetRecipesI18N()
    {
        var sql = "SELECT Id, Title, Description " +
                  "FROM recipesI18N  WHERE Culture = @Culture";

        var parameters = new DynamicParameters(new { Culture = CultureInfo.CurrentCulture.Name });

        using (var connection = new SqliteConnection(databaseConfig.Name))
        {
            return (await connection.QueryAsync<RecipeI18N>(sql, parameters)).ToList() ?? new List<RecipeI18N>();
        };
    }

    private async Task<IList<Ingredient>> GetIngredients(long recipeId)
    {
        var sql = "SELECT Name, Quantity " +
                  "FROM ingredientsI18N WHERE RecipeId = @RecipeId AND Culture = @Culture";
        var parameters = new DynamicParameters(new { RecipeId = recipeId, Culture = CultureInfo.CurrentCulture.Name });

        using (var connection = new SqliteConnection(databaseConfig.Name))
        {
           return (await connection.QueryAsync<Ingredient>(sql, parameters)).ToList() ?? new List<Ingredient>();
        }
    }

    private async Task<IList<Step>> GetSteps(long recipeId)
    {
        var sql = "SELECT Text " +
                  "FROM stepsI18N WHERE RecipeId = @RecipeId AND Culture = @Culture " +
                  "ORDER BY \"Order\"";

        var parameters = new DynamicParameters(new { RecipeId = recipeId, Culture = CultureInfo.CurrentCulture.Name });

        using (var connection = new SqliteConnection(databaseConfig.Name))
        {
           return (await connection.QueryAsync<Step>(sql, parameters)).ToList() ?? new List<Step>();
        }
    }
}
