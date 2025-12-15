using Microsoft.AspNetCore.Mvc;
using Logic.Classes;
using Logic.Interface;
using Microsoft.AspNetCore.SignalR;

namespace Secure_Access.Controllers
{
    public class ReceptionController : Controller
    {
        private readonly IReceptionService _receptionService;
        private readonly IHubContext<AccessHub> _hubContext;

        public ReceptionController(IReceptionService receptionService, IHubContext<AccessHub> hubContext)
        {
            _receptionService = receptionService;
            _hubContext = hubContext;
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
          
            var request = _receptionService.GetAllRequests().FirstOrDefault(r => r.Id == id);
            if (request != null)
            {

                request.Status = 1; 
                await _hubContext.Clients.Group(request.Email)
                    .SendAsync("ReceiveAccessNotification", "Access Granted! You may enter.");
                _receptionService.UpdateRequestStatus(id, 1);
            }

            return RedirectToAction("ReceptionistDashboard");
        }

        [HttpPost]
        public async Task<IActionResult> RejectAccess(int id)
        {
            var request = _receptionService.GetAllRequests().FirstOrDefault(r => r.Id == id);
            if (request != null)
            {
                await _hubContext.Clients.Group(request.Email)
                    .SendAsync("ReceiveAccessNotification", "Access Denied!  Please contact reception.");
                _receptionService.UpdateRequestStatus(id, 0);
            }

            return RedirectToAction("ReceptionistDashboard");
        }
    }
}
