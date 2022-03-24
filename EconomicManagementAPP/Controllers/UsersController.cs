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
            if (users.Email == users.StandarEmail)
            {
                ModelState.AddModelError(nameof(users.Email),
                    $"User with email {users.Email} and {users.StandarEmail} are equals.");
                return View(users);
            }
            await repositorieUser.Create(users);
            return RedirectToAction("Index");
        }

        //Actualizar el Usuario
        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            
            var user = await repositorieUser.getUserById(id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> Modify(Users users)
        {
            
            var user = await repositorieUser.getUserById(users.Id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieUser.Modify(users);
            return RedirectToAction("Index");
        }

        //Eliminar User
        public async Task<IActionResult> Delete(int id)
        {

            var user = await repositorieUser.getUserById(id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {

            var user = await repositorieUser.getUserById(id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieUser.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
