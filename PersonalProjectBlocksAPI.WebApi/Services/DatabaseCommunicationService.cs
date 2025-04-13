using Dapper;
using Dtos;
using Microsoft.Data.SqlClient;
using SmartHealth.WebApi.Interfaces.Services;

namespace PersonalProjectBlocksAPI.Services
{
    public class DatabaseCommunicationService(string connectionString) : IDatabaseRepository
    {
        ///<inheritdoc/>
        public async Task Insert2DObjects(Object2DDto[] objects, string EnvironmentId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Object2D] WHERE Environment2DId = @EnvironmentId", new { EnvironmentId });
                if (objects.Length > 0)
                {
                    await sqlConnection.ExecuteAsync(
                        "INSERT INTO [Object2D] (Id, PrefabId, PositionX, PositionY, ScaleX, ScaleY, RotationZ, SortingLayer,Environment2DId) VALUES (@Id, @PrefabId, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer, @Environment2DId) "
                        , objects
                        );
                }
            }
        }

        public async Task<IEnumerable<Object2DDto>> Get2DObjects(string EnvironmentId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                return await sqlConnection.QueryAsync<Object2DDto>(
                        sql:
                        "SELECT * FROM Object2D WHERE Environment2DId = @EnvironmentId"
                        , new { EnvironmentId }
                    );
            }
        }

        public async Task InsertNewEnvironment(Environment2DDto env, string id)
        { 
            env.UserId = id;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.ExecuteAsync(
                    "INSERT INTO [Environment2D] (Id, Name, UserId) VALUES (@Id, @Name, @UserId)"
                    ,env
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

        public async Task DeleteEnvironment(string id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.ExecuteAsync(
                        sql: "DELETE FROM Object2D WHERE Environment2DId = @id; DELETE FROM Environment2D WHERE Id = @Id"
                        , new { id }
                );
            }
        }
    }
}
