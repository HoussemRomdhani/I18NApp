using Demo.Extensions;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Models;
using Repositories;
using Repositories.Models;
using Services;

namespace Demo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration["DatabaseName"] ?? string.Empty });
        builder.Services.AddSingleton<UsersRepository>();
        builder.Services.AddSingleton<RecipesRepository>();
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<RecipesService>();

        builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                      .AddNegotiate();

        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });
    
        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(culture: CultureDefaults.CultureFR, uiCulture: CultureDefaults.CultureFR);
            options.SupportedCultures = CultureDefaults.SupportedCultures;
            options.SupportedUICultures = CultureDefaults.SupportedCultures;
            options.SetDefaultCulture(CultureDefaults.CultureFR);
        });

        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

        builder.Services.AddControllersWithViews()
                        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                        .AddDataAnnotationsLocalization();

        builder.Services.AddRazorPages();

        var app = builder.Build();


        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseCookieRequestCulture();

        var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

        if (options != null)
        {
            app.UseRequestLocalization(options.Value);
        }
        else
        {
            app.UseRequestLocalization();
        }


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
