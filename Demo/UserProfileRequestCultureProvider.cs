using Microsoft.AspNetCore.Localization;
using Services;

namespace Demo.Services;

public class UserProfileRequestCultureProvider : RequestCultureProvider
{
    private const string defaultCulture = "fr-FR";

    public UserProfileRequestCultureProvider()
    {
    }

    public override async Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        UserService? userService = httpContext.RequestServices.GetService<UserService>();
       
        string culture = userService != null ? await userService.GetUserCulture() : defaultCulture;

        return new ProviderCultureResult(culture);
    }
}


