namespace Demo.Extensions;

public static class CultureExtensions
{
    public static IApplicationBuilder UseCookieRequestCulture(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CookieRequestCultureMiddleware>();
    }
}
