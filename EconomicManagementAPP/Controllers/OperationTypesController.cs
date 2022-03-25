using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class OperationTypesController :Controller
    {
        private readonly IRepositorieOperationTypes repositorieOperationTypes;

        public OperationTypesController(IRepositorieOperationTypes repositorieOperationTypes)
        {
            this.repositorieOperationTypes = repositorieOperationTypes;
        }
        public async Task<IActionResult> Index()
        {
            var operation = await repositorieOperationTypes.getOperation();
            return View(operation);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(OperationTypes operationTypes)
        {
            if (!ModelState.IsValid)
            {
                return View(operationTypes);
            }

            await repositorieOperationTypes.Create(operationTypes);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificaryOperationTypes(string Description)
        {

            var operationExist = await repositorieOperationTypes.Exist(Description);

            if (operationExist)
            {
                // permite acciones directas entre front y back
                return Json($"The account {Description} already exist");
            }

            return Json(true);
        }


        //Actualizar
        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {

            var operationType = await repositorieOperationTypes.getOperationById(id);

            if (operationType is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(operationType);
        }
        [HttpPost]
        public async Task<ActionResult> Modify(OperationTypes operationTypes)
        {
            var accountType = await repositorieOperationTypes.getOperationById(operationTypes.Id);

            if (accountType is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieOperationTypes.Modify(operationTypes);// el que llega
            return RedirectToAction("Index");
        }
        // Eliminar
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var operation = await repositorieOperationTypes.getOperationById(id);

            if (operation is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(operation);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteOperation(int id)
        {

            var operation = await repositorieOperationTypes.getOperationById(id);

            if (operation is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieOperationTypes.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
