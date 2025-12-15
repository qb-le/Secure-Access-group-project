using Logic.Service;
using Microsoft.AspNetCore.Mvc;

namespace Secure_Access.Controllers
{
    public class AuditLogController : Controller
    {
        private readonly AuditLogService _auditLogService;

        public ActionResult AuditLog()
        {
            
            var role = HttpContext.Session.GetString("Role");
            
            if (role != "Receptionist")
            {
                return RedirectToAction("Index", "Login");
            }
            
            return View("~/Views/AuditLog/Index.cshtml");

        }
        
        public AuditLogController(AuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        public IActionResult Index()
        {
            var logs = _auditLogService.GetAllLogs();
            return View(logs);
        }
    }
}
