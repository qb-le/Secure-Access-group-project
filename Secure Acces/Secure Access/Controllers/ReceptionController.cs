using Microsoft.AspNetCore.Mvc;
using Logic.Classes;
using Logic.Service;
using Logic.Interface;

namespace Secure_Access.Controllers;

public class ReceptionController : Controller
{
    private readonly IReceptionService _receptionService;
    
    public ReceptionController(IReceptionService receptionService)
    {
        _receptionService = receptionService;
    }
    
    public ActionResult ReceptionistDashboard()
    {
        var requests = _receptionService.GetAllRequests();
        return View(requests);
    }

    public IActionResult Grant(int id)
    {
        //_receptionService.GrantAccess(id);
        return RedirectToAction("ReceptionistDashboard");
    }

    public IActionResult Reject(int id)
    {
        //_receptionService.RejectAccess(id);
        return RedirectToAction("ReceptionistDashboard");
    }
}