using Dtos;

namespace SmartHealth.WebApi.Interfaces.Services
{
    public interface IDatabaseRepository
    {
        /// <summary>
        /// Insert Data in DB
        /// </summary>
        /// <param name="jsonObject">World</param>
        /// <returns></returns>
        Task Insert2DObjects(Object2DDto[] objects, string EnvId);

        /// <summary>
        /// Get the environment2D's of the given userId
        /// </summary>
        /// <param name="id">of user</param>
        /// <returns>Environment2D</returns>
        Task<IEnumerable<Environment2DDto>> GetEnvironment2D(string id);

        /// <summary>
        /// Update za world
        /// </summary>
        /// <param name="jsonObject">Update values</param>
        /// <returns></returns>
        Task UpdateAsync(string jsonObject);
    }
}
