using Logic.Dto;
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
            List<DtoAuditLog> logs = _auditLogService.GetAllLogsDto();
            return View(logs);
        }

        public IActionResult ByUser(int userId)
        {
            List<DtoAuditLog> logs = _auditLogService.GetLogsByUserDto(userId);
            return View("Index", logs);
        }

        public IActionResult ByDoor(int doorId)
        {
            List<DtoAuditLog> logs = _auditLogService.GetLogsByDoorDto(doorId);
            return View("Index", logs);
        }
    }
}
