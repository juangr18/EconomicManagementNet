using EconomicManagementAPP.Models;
using EconomicManagementAPP.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomicManagementAPP.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly IRepositorieUsers repositorieUsers;
        private readonly IRepositorieAccounts repositorieAccounts;
        private readonly IRepositorieTransactions repositorieTransactions;
        private readonly IRepositorieCategories repositorieCategories;

        public TransactionsController(IRepositorieUsers repositorieUsers, IRepositorieAccounts repositorieAccounts, IRepositorieCategories repositorieCategories, IRepositorieTransactions repositorieTransactions)
        {
            this.repositorieUsers = repositorieUsers;
            this.repositorieAccounts = repositorieAccounts;
            this.repositorieTransactions = repositorieTransactions;
            this.repositorieCategories = repositorieCategories;
        }

        public async Task<IActionResult> Index()
        {
            // var transactions = await repositorieTransactions.GetTransactions();
            // return View(transactions);
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var userId = repositorieUsers.GetUserId();
            var model = new CreateTransactionViewModel();
            model.Accounts = await GetAccounts(userId);
            model.Categories = await GetCategories(userId, model.OperationTypesId);
            return View(model);
        }

        private async Task<IEnumerable<SelectListItem>> GetAccounts(int userId)
        {
            var accounts = await repositorieAccounts.GetUserAccounts(userId);
            return accounts.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> GetCategories(int userId, OperationTypes operationTypes)
        {
            var categories = await repositorieCategories.GetCategories(userId, operationTypes);
            return categories.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }

        [HttpPost]
        public async Task<IActionResult> GetCategories([FromBody] OperationTypes operationTypes)
        {
            var userId = repositorieUsers.GetUserId();
            var categories = await GetCategories(userId, operationTypes);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transactions transaction)
        {

            if (!ModelState.IsValid)
            {
                return View(transaction);
            }
            transaction.UserId = 1;
            transaction.AccountId = 1;
            transaction.CategoryId = 1;
            transaction.TransactionDate = DateTime.Now;
            await repositorieTransactions.Create(transaction);
            return RedirectToAction("Index");
        }

        //Actualizar
        [HttpGet]
        public async Task<ActionResult> Modify(int Id, int UserId)
        {
            var transactions = await repositorieTransactions.GetTransactionById(Id, UserId);

            if (transactions is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(transactions);
        }

        [HttpPost]
        public async Task<ActionResult> Modify(Transactions transactions)
        {
            var transactionsDB = await repositorieTransactions.GetTransactionById(transactions.Id, transactions.UserId);

            if (transactionsDB is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieTransactions.ModifyTransaction(transactions);
            return RedirectToAction("Index");
        }

        //Eliminar Categories
        public async Task<IActionResult> Delete(int Id, int UserId)
        {
            var transactions = await repositorieTransactions.GetTransactionById(Id, UserId);

            if (transactions is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTransactions(int Id, int UserId)
        {
            var transaction = await repositorieTransactions.GetTransactionById(Id, UserId);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieTransactions.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
