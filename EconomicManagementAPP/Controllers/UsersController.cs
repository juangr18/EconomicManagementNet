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

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

    }

}
