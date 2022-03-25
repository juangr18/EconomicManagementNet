

using Dapper;
using EconomicManagementAPP.Models;
using Microsoft.Data.SqlClient;

namespace EconomicManagementAPP.Services
{
    public interface IRepositorieUser
    {
        Task Create(Users users);
        Task<bool> Exist(string Email);
        Task<IEnumerable<Users>> getUsers();
<<<<<<< HEAD

        Task<Users> getUsersById(int id);


=======
        Task Modify(Users users); // Task para Modificar
        Task<Users> getUserById(int id); // Para obtener el usuario por id
>>>>>>> 5a4601c4c1bf7c083d9522974e956c4c843def29
        Task Delete(int id);
    }

    public class RepositorieUser : IRepositorieUser
    {
        private readonly string connectionString;

        public RepositorieUser(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
<<<<<<< HEAD

       

        public Task<IEnumerable<Users>> getUsers()
=======
        public async Task Create(Users users)
>>>>>>> 5a4601c4c1bf7c083d9522974e956c4c843def29
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO Users
                                                        (Email, StandarEmail,Password)
                                                        VALUES(@Email, @StandarEmail, @Password); SELECT SCOPE_IDENTITY();",
                                                                users);
            users.Id = id;
        }

        public async Task<bool> Exist(string Email)
        {
            using var connection = new SqlConnection(connectionString);
            var exist = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1
                                                                      FROM Users
                                                                      WHERE Email = @Email;",
                                                                      new { Email });
            return exist == 1;
        }
        public async Task<IEnumerable<Users>> getUsers()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Users>(@"SELECT Id, Email, StandarEmail
                                                    FROM Users;");
        }

        //Para Actualizar se necesita obtener el tipo de cuenta por el id
        public async Task<Users> getUserById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Users>(@"
                                                                Select Id,Email,StandarEmail,Password From Users
                                                                WHERE Id = @Id",
                                                                new { id });
        }
        // Actualizar
        public async Task Modify(Users users)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"Update Users set Email = @email, StandarEmail = @StandarEmail, Password = @Password  WHERE Id = @Id;", users);
        }

        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE Users WHERE Id = @Id", new { id });
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
