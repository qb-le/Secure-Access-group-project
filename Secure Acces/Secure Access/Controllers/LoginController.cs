using Microsoft.AspNetCore.Mvc;
using Logic.Services;
using Microsoft.AspNetCore.Connections;

namespace Secure_Access.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;

        public LoginController(UserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }
    }
}
