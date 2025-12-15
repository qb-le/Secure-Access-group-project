using Logic.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Secure_Access.Controllers
{
    public class DoorController : Controller
    {
        private readonly IDoorService _doorService;

        public DoorController(IDoorService doorService)
        {
            _doorService = doorService;
        }
        public ActionResult ChooseDoor()
        {
            
            var role = HttpContext.Session.GetString("Role");
            
            if (role != "User")
            {
                return RedirectToAction("Index", "Login");
            }
            
            return View("~/Views/Door/Index.cshtml");

        }
        public IActionResult Index()
        {
            var groups = _doorService.GetAllDoorGroups();
            return View(groups);
        }

        public IActionResult GroupDetails(int id)
        {
            var doors = _doorService.GetDoorsByGroupId(id);
            ViewBag.GroupId = id;
            return View(doors);
        }

        public IActionResult DoorDetails(int id)
        {
            var door = _doorService.GetDoorById(id);
            if (door == null) return NotFound();
            return View(door);
        }
    }
}