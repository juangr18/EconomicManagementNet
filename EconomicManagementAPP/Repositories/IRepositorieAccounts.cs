using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.Repositories
{
    public interface IRepositorieAccounts
    {
        Task Create(Accounts accounts);
        Task<IEnumerable<Accounts>> GetAccounts();
        Task ModifyAccount(Accounts accounts);
        Task Delete(int Id);

    }
}
