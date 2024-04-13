using System.Security.Claims;

namespace ConverseSpace.API.Extensions;

public static class HttpContextExtension
{
    public static Guid GetUserId(this HttpContext context)
    {
        if (context.Items.TryGetValue("UserId", out var userId) && userId is Guid)
        {
            return (Guid)userId;
        }
        return Guid.Empty;
    }
}