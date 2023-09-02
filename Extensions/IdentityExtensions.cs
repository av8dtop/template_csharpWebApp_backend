using System.Security.Claims;
using csharp_demo_backend.Models;

namespace csharp_demo_backend.Extensions
{
    public static class IdentityExtensions
    {
        public static User GetUser(this ClaimsPrincipal user)
        {
            // Get the User object from the ClaimsPrincipal
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = user.FindFirst(ClaimTypes.Name)?.Value;
            var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;

            return new User
            {
                Id = userId,
                Name = userName,
                Email = userEmail
            };
        }
    }
}
