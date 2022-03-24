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
        Task Modify(Users users); // Task para Modificar
        Task<Users> getUserById(int id); // Para obtener el usuario por id
        Task Delete(int id);

    }

    public class RepositorieUser : IRepositorieUser
    {
        private readonly string connectionString;

        public RepositorieUser(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            {

                ic async Task Create(Users users)
                {
                    using var connection = new SqlConnection(connectionString);
                    var id = await connection.QuerySingleAsync<int>($@"INSINTO Users
                                                    (Email, StandarEmail,Password)
    }

        users.Id = id;
    {

                ic async Task<bool> Exist(string Email)
        {
    }

    //Delete
                                                                  WHERE Email = @Email;",
            {
                        return exist == 1;


                        public async Task<IEnumerable<Users>> getUsers()
                        {
                        }
                        // Actualizar
                        FROM Users; ");
    {

                            //Delete
                            public async Task Delete(int id)

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

                            public async Task Modify(Users users)
                            {
                                using var connection = new SqlConnection(connectionString);
                                await connection.ExecuteAsync("DELETE Users WHERE Id = @Id", new { id });
                            }
                        }
                    }
