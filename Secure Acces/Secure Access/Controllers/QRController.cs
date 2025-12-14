using Logic.Classes;
using Logic.Dto;
using Logic.Interface;
using Logic.Service;
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
        private readonly AuditLogService _auditLogService;
        private readonly QRTokenManager _qrManager;
        private readonly Email _email;
        private readonly IHubContext<AccessHub> _hubContext;
        private readonly IReceptionService _receptionService;

        // Inject the manager
        public QRController(QRTokenManager qrManager, IHubContext<AccessHub> hubContext, IReceptionService receptionService, AuditLogService auditLogService)
        {
            _qrManager = qrManager;
            _email = new Email();
            _hubContext = hubContext;
            _receptionService = receptionService;
            _auditLogService = auditLogService;
        }

        public IActionResult Scanner()
        {
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

                _auditLogService.LogDoorOpenRequest(new DtoAuditLog
                {
                    UserId = 0, //not yet implemented sessions
                    DoorId = info.DoorId,
                });

                var request = new Request(
                    info.Name,
                    info.Email,
                    info.DoorId,
                    DateTime.UtcNow,
                    2
                );

                await _receptionService.AddRequestAsync(request);

                await _hubContext.Clients.Group("Receptionists")
                        .SendAsync("ReceiveNotification", _receptionService.GetLatestRequest());

                return Json(new
                {
                    success = true,
                    email = info.Email,
                    message = "Scan successful! Waiting for access..."
                });
            }

            return Json(new { success = false, message = "Invalid QR token." });
        }


        public IActionResult CheckScan(string token)
        {
            bool scanned = _qrManager.IsScanned(token);
            return Json(new { scanned });
        }
    }
}
