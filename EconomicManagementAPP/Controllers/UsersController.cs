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



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var user = await repositorieUser.getUsersById(id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {

            var user = await repositorieUser.getUsersById(id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieUser.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
