using Microsoft.AspNetCore.Mvc;
using Services;

namespace Demo.Controllers;

public class HomeController : Controller
{
    private readonly RecipesService recipesService;
    public HomeController(RecipesService recipesService)
    {
        this.recipesService = recipesService;
    }

    public IActionResult Index() => View();

    public async Task<IActionResult> Recipes() => View(await recipesService.GetRecipes());
}
