using Logic.Classes;
using Logic.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QRCoder;
using Secure_Access.Services;
using System.Drawing.Imaging;
using System.IO;
using static QRCoder.PayloadGenerator;

namespace Secure_Access.Controllers
{
    public class QRController : Controller
    {
        private readonly QRTokenManager _qrManager;
        private readonly Email _email;
        private readonly IHubContext<AccessHub> _hubContext;
        private readonly IReceptionService _receptionService;

        // Inject the manager
        public QRController(QRTokenManager qrManager, IHubContext<AccessHub> hubContext, IReceptionService receptionService)
        {
            _qrManager = qrManager;
            _email = new Email();
            _hubContext = hubContext;
            _receptionService = receptionService;
        }

        public IActionResult Scanner()
        {
            return View();
        }

        public IActionResult QRLogin()
        {
            //var qrToken = _qrManager.GenerateToken();
            //ViewBag.QRToken = qrToken;
            return View();
        }

        public IActionResult GenerateQRCode(string token)
        {
            var url = Url.Action("Scan", "QR", new { token }, Request.Scheme);

            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrData);
            using var bitmap = qrCode.GetGraphic(20);
            using var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            return File(ms.ToArray(), "image/png");
        }

        public IActionResult SendQR(int doorId, string receiverEmail, string receiverName)
        {
            var token = _qrManager.GenerateToken(receiverName, receiverEmail, doorId);

            var url = Url.Action("Scan", "QR", new
            {
                token,
                name = receiverName,
                email = receiverEmail,
                doorId
            }, Request.Scheme);

            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrData);
            using var bitmap = qrCode.GetGraphic(20);
            using var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            var htmlTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EmailTemplates", "QREmailTemplate.html");
            var htmlTemplate = System.IO.File.ReadAllText(htmlTemplatePath);

            _email.SendEmailWithQR(receiverName, receiverEmail, htmlTemplate, ms.ToArray());

            TempData["Message"] = $"QR code sent to {receiverEmail}.";
            return RedirectToAction("DoorDetails", "Door", new { id = doorId });
        }

        public async Task<IActionResult> Scan(string token)
        {
            if (_qrManager.MarkAsScanned(token))
            {
                var info = _qrManager.GetInfo(token);

                var request = new Request(
                    info.Name,
                    info.Email,
                    info.DoorId,
                    DateTime.UtcNow,
                    2
                );

                await _receptionService.AddRequestAsync(request);
                await _hubContext.Clients.Group("Receptionists")
                        .SendAsync("ReceiveNotification", request);

                return Content("Scan successful! You can close the phone browser.");
            }

            return Content("Invalid QR token.");
        }

        public IActionResult CheckScan(string token)
        {
            bool scanned = _qrManager.IsScanned(token);
            return Json(new { scanned });
        }
    }
}
