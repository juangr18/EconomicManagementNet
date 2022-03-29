using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomicManagementAPP.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly IRepositorieCategories repositorieCategories;
        private readonly IRepositorieOperationTypes repositorieCategoriesOperationTypes;


        public CategoriesController(IRepositorieOperationTypes repositorieCategoriesOperationTypes, IRepositorieCategories repositorieCategories)
        {
            this.repositorieCategoriesOperationTypes = repositorieCategoriesOperationTypes;
            this.repositorieCategories = repositorieCategories;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            var operationType = await repositorieCategoriesOperationTypes.getOperation();
            var model = new CategorieTypeOperationViewModel();
            model.OperationType = operationType.Select(x => new SelectListItem(x.Description, x.Id.ToString()));
            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            var categories = await repositorieCategories.getCategories();
            return View(categories);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Categories categorie)
        {
            if (!ModelState.IsValid)
            {
                return View(categorie);
            }
            var categorieExist = await repositorieCategories.Exist(categorie.Name);
            if (categorieExist)
            {
                ModelState.AddModelError(nameof(categorie.Name),
                    $"User with email {categorie.Name} already exist.");
                return View(categorie);
            }
            categorie.OperationTypeId = 1;
            categorie.UserId = 1;
            await repositorieCategories.Create(categorie);
            return RedirectToAction("Index");
        }

        //Actualizar
        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            var categorie = await repositorieCategories.getCategorieById(id);

            if (categorie is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(categorie);
        }

        [HttpPost]
        public async Task<ActionResult> Modify(Categories categorie)
        {
            var categorieDB = await repositorieCategories.getCategorieById(categorie.Id);

            if (categorieDB is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieCategories.Modify(categorie);
            return RedirectToAction("Index");
        }

        //Eliminar Categories
        public async Task<IActionResult> Delete(int id)
        {
            var categories = await repositorieCategories.getCategorieById(id);

            if (categories is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(categories);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            var categorie = await repositorieCategories.getCategorieById(id);

            if (categorie is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieCategories.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
