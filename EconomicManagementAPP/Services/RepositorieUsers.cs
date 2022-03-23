using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace EconomicManagementAPP.Services
{
    public interface IRepositorieUser
    {
        Task Create(Users users);
        Task<bool> Exist(string Email, int UserId);
        Task<IEnumerable<Users>> getUsers();
    }

    public class RepositorieUser : IRepositorieUser
    {
        private readonly string connectionString;

        public RepositorieUser(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(Users users)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO Users
                                                        (Email, StandarEmail,Password)
                                                        VALUES(@Email, @StandarEmail, @Password); SELECT SCOPE_IDENTITY();", 
                                                        users);
            users.Id = id;
        }
        public async Task Exist(string Email, int UserId)
        {

        }
        public Task<IEnumerable<Users>> getUsers()
        {
            throw new NotImplementedException();
        }
    }
}
