using System.Security.Claims;

namespace ConverseSpace.API.Middlewares;

public class UserInfoMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    
    public async Task InvokeAsync(HttpContext context)
    {
        var userIdClaim = context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            context.Items["UserId"] = userId;
        }

        await _next(context);
    }
}