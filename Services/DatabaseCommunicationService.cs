using Dapper;
using Dtos;
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
        public async Task<IEnumerable<Environment2DDto>> GetEnvironment2D(string id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                return await sqlConnection.QueryAsync<Environment2DDto>(
                        sql: "SELECT * FROM [Environment2D] WHERE UserId = @id;"
                        , new { id }
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
