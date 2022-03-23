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
<<<<<<< HEAD
            return View(users);
        }
=======
>>>>>>> 41968cc37c7e95fe1b6c562bc10f552cf1b90213

            return View(users);
        }
<<<<<<< HEAD
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
=======

>>>>>>> 41968cc37c7e95fe1b6c562bc10f552cf1b90213

        // public async Task<IActionResult> Create(Users users)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View(users);
        //     }
        //     var userExist = await repositorieUser.Exist(users.Id);
        //     return userExist;
        // }
    }
}
