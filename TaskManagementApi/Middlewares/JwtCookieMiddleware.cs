using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class JwtCookieMiddleware
{
    private readonly RequestDelegate _next;

    public JwtCookieMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Cookies["jwt"];

        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers.Append("Authorization", "Bearer " + token);
        }

        await _next(context);
    }
}
