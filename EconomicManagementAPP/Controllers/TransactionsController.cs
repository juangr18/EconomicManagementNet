using EconomicManagementAPP.Models;
using EconomicManagementAPP.Repositories;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomicManagementAPP.Controllers
{
    public class TransactionsController : Controller
    {

        private readonly IRepositorieTransactions repositorieTransactions;
        private readonly IRepositorieOperationTypes repositorieOperationTypes;

        public TransactionsController(IRepositorieTransactions repositorieTransactions, IRepositorieOperationTypes repositorieOperationTypes)
        {
            this.repositorieTransactions = repositorieTransactions;
            this.repositorieOperationTypes = repositorieOperationTypes;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await repositorieTransactions.GetTransactions();
            return View(transactions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transactions transaction)
        {
            var operationTypesDB = await repositorieOperationTypes.getOperation();
            ViewBag.message = operationTypesDB.ToList();

            if (!ModelState.IsValid)
            {
                return View(transaction);
            }
            transaction.UserId = 1;
            transaction.AccountId = 1;
            transaction.OperationTypeId = 1;
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
