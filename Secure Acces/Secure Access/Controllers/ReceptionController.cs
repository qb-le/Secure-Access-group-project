using Microsoft.AspNetCore.Mvc;
using Logic.Classes;
using Logic.Interface;
using Microsoft.AspNetCore.SignalR;
using Logic.Dto;
using Logic.Service;

namespace Secure_Access.Controllers
{
    public class ReceptionController : Controller
    {
        private readonly IReceptionService _receptionService;
        private readonly IHubContext<AccessHub> _hubContext;
        private readonly AuditLogService _auditLogService;

        public ReceptionController(IReceptionService receptionService, IHubContext<AccessHub> hubContext, AuditLogService auditLogService)
        {
            _receptionService = receptionService;
            _hubContext = hubContext;
            _auditLogService = auditLogService;
        }
        public ActionResult ReceptionistDashboard()
        {
    
            var role = HttpContext.Session.GetString("Role");
    
            if (role != "Receptionist")
            {
                return RedirectToAction("Index", "Login");
            }
    
            var requests = _receptionService.GetAllRequests();
            return View(requests);
        }
        
        [HttpPost]
        public async Task<IActionResult> GrantAccess(int id)
        {
            // Retrieve request info
            var request = _receptionService.GetRequestById(id);
            if (request != null)
            {

                request.Status = 1; // 1 = granted, 2 = pending, 3 = rejected
                await _hubContext.Clients.Group(request.Email)
                    .SendAsync("ReceiveAccessNotification", "Access Granted! You may enter.");
                _receptionService.UpdateRequestStatus(id, 1);

                var dto = new DtoAuditLog
                {
                    //UserId = request.UserId
                    DoorId = request.DoorId
                };

                _auditLogService.LogDoorAccessGranted(dto);
            }

            return RedirectToAction("ReceptionistDashboard");
        }

        [HttpPost]
        public async Task<IActionResult> RejectAccess(int id)
        {
            var request = _receptionService.GetRequestById(id);
            if (request != null)
            {
                await _hubContext.Clients.Group(request.Email)
                    .SendAsync("ReceiveAccessNotification", "Access Denied!  Please contact reception.");
                _receptionService.UpdateRequestStatus(id, 0);

                var dto = new DtoAuditLog
                {
                    //UserId = request.UserId
                    DoorId = request.DoorId
                };

                _auditLogService.LogDoorAccessDenied(dto);
            }

            return RedirectToAction("ReceptionistDashboard");
        }
    }
}
