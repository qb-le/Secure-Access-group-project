using Microsoft.AspNetCore.Mvc;
using Logic.Services;

namespace Secure_Access.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _employeeService;

        public LoginController()
        {
            string connectionString = "YourConnectionStringHere";
            _employeeService = new UserService(connectionString);
        }

        public ActionResult Index(int employeeId)
        {
            var employeeDto = _employeeService.GetEmployee(employeeId);
            ViewBag.EmployeeName = employeeDto.Name;
            return View();
        }
    }
}
