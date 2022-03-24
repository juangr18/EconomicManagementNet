

using Dapper;
using EconomicManagementAPP.Models;
using Microsoft.Data.SqlClient;

namespace EconomicManagementAPP.Services
{
    public interface IRepositorieUser
    {
        Task<IEnumerable<Users>> getUsers();

        Task<Users> getUsersById(int id);


        Task Delete(int id);
    }

    public class RepositorieUser : IRepositorieUser
    {
        private readonly string connectionString;

        public RepositorieUser(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

       

        public Task<IEnumerable<Users>> getUsers()
        {
            throw new NotImplementedException();
        }


        public async Task<Users> getUsersById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Users>(@"
                                                                SELECT Id, Email, EstandarEmail, Password 
                                                                FROM Users
                                                                WHERE Id = @Id",
                                                                new { id });
        }


        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE Users WHERE Id = @Id", new { id });
        }

      
    }
}
