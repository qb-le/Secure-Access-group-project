using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using Secure_Access.Services;

namespace Secure_Access.Controllers
{
    public class QRController : Controller
    {
        private readonly QRTokenManager _qrManager;

        // Inject the manager
        public QRController(QRTokenManager qrManager)
        {
            _qrManager = qrManager;
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
