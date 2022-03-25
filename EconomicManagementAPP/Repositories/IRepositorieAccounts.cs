using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.Repositories
{
    public interface IRepositorieAccounts
    {
        Task Create(Accounts accounts);
        Task<bool> Exist(string Name, int Id);
        Task<IEnumerable<Accounts>> GetAccounts();
        Task<IEnumerable<Accounts>> GetAccountsById(int Id);
        Task Modify(Accounts accounts);
        Task Delete(int Id);

    }
}
