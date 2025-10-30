using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using Secure_Access.Services;
using Logic.Classes;

namespace Secure_Access.Controllers
{
    public class QRController : Controller
    {
        private readonly QRTokenManager _qrManager;
        private readonly Email _email;

        // Inject the manager
        public QRController(QRTokenManager qrManager)
        {
            _qrManager = qrManager;
            _email = new Email();
        }

        public IActionResult QRLogin()
        {
            var qrToken = _qrManager.GenerateToken();
            ViewBag.QRToken = qrToken;
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
            var token = _qrManager.GenerateToken();
            var url = Url.Action("Scan", "QR", new { token }, Request.Scheme);

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



        public IActionResult Scan(string token)
        {
            if (_qrManager.MarkAsScanned(token))
            {
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
