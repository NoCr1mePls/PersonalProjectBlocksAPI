using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHealth.WebApi.Interfaces.Services;

namespace SmartHealth.WebApi.Controllers
{

    /// <summary>
    /// Get your own UserID (This is highly cyberinsecure and should not be messed with)
    /// </summary>
    /// <param name="auth"></param>
    [ApiController]
    [Route("[controller]")]
    public class UserInformationController(IAuthenticationService auth) : ControllerBase
    {
        [HttpGet(Name = "GetUserInformation")]
        [Authorize]
        public string GetUser() => auth.GetCurrentAuthenticatedUserID();
       
    }
}
