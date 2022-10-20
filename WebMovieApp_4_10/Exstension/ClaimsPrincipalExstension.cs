using System.Security.Claims;

namespace WebMovieApp_4_10.Exstension
{
    public static class ClaimsPrincipalExstension
    {
        public static string GetFullName(this ClaimsPrincipal principal)
        {
            var fullName = principal.Claims.FirstOrDefault(c => c.Type == "FullName");
            return fullName?.Value;
        }
    }
}
