using Dapper;
using Microsoft.Data.SqlClient;
using SmartHealth.WebApi.Interfaces.Services;

namespace PersonalProjectBlocksAPI.Services
{
    public class DatabaseCommunicationService(string connectionString) : IDatabaseRepository
    {
        ///<inheritdoc/>
        public async Task InsertAsync(string jsonObject)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.ExecuteAsync(
                    ""
                    );
            }
        }

        ///<inheritdoc/>
        public async Task<string?> ReadAsync(string id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<string>(
                        ""
                );
            }
        }

        ///<inheritdoc/>
        public async Task UpdateAsync(string jsonObject)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.ExecuteAsync(
                    ""
                    );
            }
        }
    }
}
