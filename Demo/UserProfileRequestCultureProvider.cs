using Microsoft.AspNetCore.Localization;
using Models;
using Services;

namespace Demo.Services;

public class UserProfileRequestCultureProvider : RequestCultureProvider
{
    public UserProfileRequestCultureProvider()
    {
    }

    public override async Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        UserService? userService = httpContext.RequestServices.GetService<UserService>();

        string culture = userService != null ? await userService.GetUserCultureOrDefault() : CultureDefaults.CultureFR;

        return new ProviderCultureResult(culture);
    }
}


