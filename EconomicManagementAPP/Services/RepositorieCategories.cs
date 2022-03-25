using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace EconomicManagementAPP.Services
{
    public interface IRepositorieCategories
    {
        Task Create(Categories categories);

        Task<bool> Exist(string Name);

        Task<IEnumerable<Categories>> getCategories();

        Task Modify(Categories categories);

        Task<Categories> getCategorieById(int id);

        Task Delete(int id);

    }

    public class RepositorieCategories : IRepositorieCategories
    {
        private readonly string connectionString;

        public RepositorieCategories(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task Create(Categories categories)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO Categories
                                                        (Name, OperationId, UserId)
                                                        VALUES(@Name, @OperationId, @User); SELECT SCOPE_IDENTITY();",
                                                                categories);
            categories.Id = id;
        }

        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE Categories WHERE Id = @Id", new { id });
        }

        public async Task<bool> Exist(string Name)
        {
            using var connection = new SqlConnection(connectionString);

            var exist = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1
                                                                      FROM Categories
                                                                      WHERE Name = @Name;",
                                                                      new { Name });
            return exist == 1;
        }


        public async Task<Categories> getCategorieById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Categories>(@"SELECT Id, Name, ObjectId, UserId
                                                                    FROM Categories
                                                                    WHERE Id = @Id",
                                                                    new { id });
        }

        public async Task<IEnumerable<Categories>> getCategories()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Categories>(@"SELECT Id, Name, OperationTypeId, UserId
                                                    FROM Categories;");
        }

        public async Task Modify(Categories categories)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Categories
                                            SET Name = @Name,
                                            OperationTypeId = @OperationTypeId
                                            WHERE Id = @Id", categories);
        }
    }
}