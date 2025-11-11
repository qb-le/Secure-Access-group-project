using Logic.Service;
using Microsoft.AspNetCore.Mvc;

namespace Secure_Access.Controllers
{
    public class AuditLogController : Controller
    {
        private readonly AuditLogService _auditLogService;

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
