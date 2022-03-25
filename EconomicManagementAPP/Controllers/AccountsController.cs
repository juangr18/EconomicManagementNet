using EconomicManagementAPP.Models;
using EconomicManagementAPP.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class AccountsController : Controller
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

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Accounts accounts)
        {
            if (!ModelState.IsValid)
            {
                return View(accounts);
            }
            var accountsExist =
               await repositorieAccounts.Exist(accounts.Name, accounts.AccountTypeId);
            if (accountsExist)
            {
                ModelState.AddModelError(nameof(accounts.Name),
                    $"The account {accounts.Name} already exist.");
                return View(accounts);
            }
            await repositorieAccounts.Create(accounts);
            return RedirectToAction("Index");
        }
    }
}
