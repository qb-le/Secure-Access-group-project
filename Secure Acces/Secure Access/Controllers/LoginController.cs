using Microsoft.AspNetCore.Mvc;
using Logic.Services;
using Microsoft.AspNetCore.Connections;

namespace Secure_Access.Controllers
{
    public class LoginController : Controller
    {
        // private readonly UserService _userService;
        //
        // public LoginController(UserService userService)
        // {
        //     _userService = userService;
        // }
        //
        // public ActionResult Index()
        // {
        //     var users = _userService.GetAllUsers();
        //     return View(users);
        // }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Set(string role)
        {
            if (role != "User" && role != "Receptionist")
                return RedirectToAction("Index");

            HttpContext.Session.SetString("Role", role);

            return role == "Receptionist"
                ? RedirectToAction("ReceptionistDashboard", "Reception")
                : RedirectToAction("Index", "Door");
        }

        [HttpPost]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Role");
            return RedirectToAction("Index");
        }
    }
}
