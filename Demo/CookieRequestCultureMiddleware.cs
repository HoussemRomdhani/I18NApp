using Microsoft.AspNetCore.Localization;
using Services;
using System.Globalization;

namespace Demo;

public class CookieRequestCultureMiddleware
{
    private readonly RequestDelegate next;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly UserService userService;
    public CookieRequestCultureMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, UserService userService)
    {
        this.next = next;
        this.httpContextAccessor = httpContextAccessor;
        this.userService = userService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var user = context.User.Identity;

        bool isAuthenticated = user != null && user.IsAuthenticated;

        bool existsCookieRequestCulture = httpContextAccessor.HttpContext != null &&
                                          httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(CookieRequestCultureProvider.DefaultCookieName, out _);

        if (isAuthenticated && !existsCookieRequestCulture)
        {
            string culture = await userService.GetUserCultureOrDefault();

            string cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(new CultureInfo(culture)));

            context.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, cookieValue);
        }

        await next(context);
    }
}
