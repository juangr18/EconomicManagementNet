using EconomicManagementAPP.Models;


namespace EconomicManagementAPP.Services
{
    public interface IRepositorieUser
    {
        Task Delete(int id);

    }

    public class RepositorieUser : IRepositorieUser
    {
        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE Users WHERE Id = @Id", new { id });
        }
    }

}
