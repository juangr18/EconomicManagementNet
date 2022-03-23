using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace EconomicManagementAPP.Services
{
    public interface IRepositorieUser
    {
        Task Create(Users users);
        Task<IEnumerable<Users>> getUsers();
    }

    public class RepositorieUser : IRepositorieUser
    {
        private readonly string connectionString;

        public RepositorieUser(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

<<<<<<< HEAD
        public async Task Create(Users users)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO Users
                                                        (Email, StandarEmail,Password)
                                                        VALUES(@Email, @StandarEmail, @Password); SELECT SCOPE_IDENTITY();", 
                                                        users);
            users.Id = id;
        }
        public Task<IEnumerable<Users>> getUsers()
=======
        public async Task<IEnumerable<Users>> getUsers()
>>>>>>> 79a5f89 (Feature: Index flow add)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Users>(@"SELECT Id, Email, StandarEmail
                                                    FROM Users");
        }
    }
}
