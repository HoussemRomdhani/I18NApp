
using Models;
using Repositories;

namespace Services;

public class RecipesService
{
    private readonly RecipesRepository recipesRepository;
    public RecipesService(RecipesRepository recipesRepository)
    {
        this.recipesRepository = recipesRepository;
    }

    public async Task<IEnumerable<Recipe>> GetRecipes()
    {
        IEnumerable<Recipe> result = await recipesRepository.GetRecipes();

        return result;
    }
}