using Dapper;
using EconomicManagementAPP.Models;
using Microsoft.Data.SqlClient;

namespace EconomicManagementAPP.Services
{

    public class RepositorieOperationTypes : IRepositorieOperationTypes
    {
        private readonly string connectionString;

        public RepositorieOperationTypes(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(OperationTypes operationTypes)
        {
            using var connection = new SqlConnection(connectionString);
            // Requiere el await - tambien requiere el Async al final de la query
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO OperationTypes
                                                (Description)
                                                VALUES (@Description); SELECT SCOPE_IDENTITY();", operationTypes);
            operationTypes.Id = id;
        }

        public async Task<IEnumerable<OperationTypes>> getOperation()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<OperationTypes>(@"SELECT Id, Description
                                                    FROM OperationTypes;");
        }

        public async Task<bool> Exist(string Description)
        {
            using var connection = new SqlConnection(connectionString);
            // El select 1 es traer lo primero que encuentre y el default es 0
            var exist = await connection.QueryFirstOrDefaultAsync<int>(
                                    @"SELECT 1
                                    FROM OperationTypes
                                    WHERE Description = @Description;",
                                    new { Description });
            return exist == 1;
        }

        //Para Actualizar se necesita obtener el tipo de cuenta por el id
        public async Task<OperationTypes> getOperationById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<OperationTypes>(@"
                                                                Select Id, Description From OperationTypes
                                                                WHERE Id = @Id",
                                                                new { id });
        }
        // Actualizar
        public async Task Modify(OperationTypes operationTypes)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE OperationTypes
                                            SET Description = @Description
                                            WHERE Id = @Id", operationTypes);
        }

        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE OperationTypes WHERE Id = @Id", new { id });
        }
    }
}
