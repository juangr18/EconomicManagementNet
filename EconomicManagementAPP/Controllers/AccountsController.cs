using EconomicManagementAPP.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class AccountsController: Controller
    {
        private IRepositorieAccounts repositorieAccounts;

        public AccountsController(IRepositorieAccounts repositorieAccounts)
        {
            this.repositorieAccounts = repositorieAccounts;
        }

        public async Task<IActionResult> Index()
        {
            var accounts = await repositorieAccounts.GetAccounts();
            return View(accounts);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
