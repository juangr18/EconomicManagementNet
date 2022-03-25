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
        [HttpGet]
        public async Task<IActionResult> Modify(int Id)
        {
            var account = await repositorieAccounts.GetAccountsById(Id);
            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(account);
        }
        [HttpPost]
        public async Task<IActionResult> Modify(Accounts accounts)
        {
            var account = await repositorieAccounts.GetAccountsById(accounts.Id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            await repositorieAccounts.Modify(accounts);
            return RedirectToAction("Index");
        }
    }
}
