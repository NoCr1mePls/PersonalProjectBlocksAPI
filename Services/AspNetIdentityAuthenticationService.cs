using SmartHealth.WebApi.Interfaces.Services;
using System.Security.Claims;
namespace PersonalProjectBlocksAPI.Services
{
    public class AspNetIdentityAuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AspNetIdentityAuthenticationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the current users' ID
        /// </summary>
        /// <returns>string?</returns>
        public string? GetCurrentAuthenticatedUserID()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
