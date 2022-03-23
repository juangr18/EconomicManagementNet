using EconomicManagementAPP.Models;


namespace EconomicManagementAPP.Services
{
    public interface IRepositorieUser
    {
<<<<<<< HEAD
        Task Delete(int id);

=======
        Task<IEnumerable<Users>> getUsers();
>>>>>>> e374535d5e9aea06432614be4bf5cbcd02317ef1
    }

    public class RepositorieUser : IRepositorieUser
    {
<<<<<<< HEAD
        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE Users WHERE Id = @Id", new { id });
=======
        private readonly string connectionString;

        public RepositorieUser(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Task<IEnumerable<Users>> getUsers()
        {
            throw new NotImplementedException();
>>>>>>> e374535d5e9aea06432614be4bf5cbcd02317ef1
        }
    }

}
