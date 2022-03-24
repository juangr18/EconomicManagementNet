

using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.Services
{
    public interface IRepositorieUser
    {
        Task<IEnumerable<Users>> getUsers();
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
    }
}
