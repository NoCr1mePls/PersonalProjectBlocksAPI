namespace SmartHealth.WebApi.Interfaces.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Returns the username of the authenticated user
        /// </summary>
        /// <returns>returns the username as a string</returns>
        string? GetCurrentAuthenticatedUserID();
    }
}
