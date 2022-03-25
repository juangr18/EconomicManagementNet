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
<<<<<<< HEAD
<<<<<<< HEAD



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var user = await repositorieUser.getUsersById(id);
=======
=======

>>>>>>> bcc623c4a2e8d01b333ac2614830a0c48b01f27c
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

<<<<<<< HEAD
        //Eliminar User
        public async Task<IActionResult> Delete(int id)
        {

            var user = await repositorieUser.getUserById(id);
>>>>>>> 5a4601c4c1bf7c083d9522974e956c4c843def29
=======
        // Delete User
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await repositorieUser.getAccountById(id);
>>>>>>> bcc623c4a2e8d01b333ac2614830a0c48b01f27c

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
<<<<<<< HEAD

<<<<<<< HEAD
            var user = await repositorieUser.getUsersById(id);
=======
            var user = await repositorieUser.getUserById(id);
>>>>>>> 5a4601c4c1bf7c083d9522974e956c4c843def29
=======
            var user = await repositorieUser.getAccountById(id);
>>>>>>> bcc623c4a2e8d01b333ac2614830a0c48b01f27c

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieUser.Delete(id);
            return RedirectToAction("Index");
        }
<<<<<<< HEAD
<<<<<<< HEAD


=======
>>>>>>> 5a4601c4c1bf7c083d9522974e956c4c843def29
=======
>>>>>>> bcc623c4a2e8d01b333ac2614830a0c48b01f27c
    }
}
