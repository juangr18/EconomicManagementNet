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
            var userExist = await repositorieUser.Exist(users.Email, users.Id);
            if (userExist)
            {
                ModelState.AddModelError(nameof(users.Email),
                    $"User with email {users.Email} already exist.");
                return View(users);
            }
            await repositorieUser.Create(users);
            return RedirectToAction("Index");
        }
    }
}
