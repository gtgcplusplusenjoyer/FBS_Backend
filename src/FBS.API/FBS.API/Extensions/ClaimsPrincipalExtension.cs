using System.Security.Claims;

namespace FBS.API.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
        }
    }
}
