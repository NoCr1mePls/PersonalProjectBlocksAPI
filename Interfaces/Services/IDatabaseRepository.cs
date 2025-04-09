using System.Data.Common;

namespace SmartHealth.WebApi.Interfaces.Services
{
    public interface IDatabaseRepository
    {
        /// <summary>
        /// Insert Data in DB
        /// </summary>
        /// <param name="jsonObject">World</param>
        /// <returns></returns>
        Task InsertAsync(string jsonObject);

        /// <summary>
        /// Read world
        /// </summary>
        /// <param name="id">of user</param>
        /// <returns>world json</returns>
        Task<string?> ReadAsync(string id);

        /// <summary>
        /// Update za world
        /// </summary>
        /// <param name="jsonObject">Update values</param>
        /// <returns></returns>
        Task UpdateAsync(string jsonObject);
    }
}
