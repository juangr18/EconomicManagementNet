using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class UsersController : Controller
    {

        private readonly IRepositorieUser repositorieUser;

        public UsersController(IRepositorieUser repositorieUser)
        {
            this.repositorieUser = repositorieUser;
        }

        public async Task<IActionResult> Index()
        {
            var users = await repositorieUser.getUsers();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Users users)
        {
            if (!ModelState.IsValid)
            {
                return View(users);
            }
            var userExist = await repositorieUser.Exist(users.Email);
            if (userExist)
            {
                ModelState.AddModelError(nameof(users.Email),
                    $"User with email {users.Email} already exist.");
                return View(users);
            }
            await repositorieUser.Create(users);
            return RedirectToAction("Index");
        }

        //Actualizar
        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            var user = await repositorieUser.getAccountById(id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(user);
        }

        // Hace que la validacion se active automaticamente desde el front
        // [HttpGet]
        // public async Task<IActionResult> Check (string Email, string StandarEmail)
        // {
        //     var accountTypeExist = await repositorieUser.Exist(, );
        //
        //     if (accountTypeExist)
        //     {
        //         // permite acciones directas entre front y back
        //         return Json($"The account {Name} already exist");
        //     }
        //
        //     return Json(true);
        // }

        [HttpPost]
        public async Task<ActionResult> Modify(Users users)
        {
            var user = await repositorieUser.getAccountById(users.Id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieUser.Modify(users);// el que llega
            return RedirectToAction("Index");
        }

        // Delete User
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await repositorieUser.getAccountById(id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await repositorieUser.getAccountById(id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieUser.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
