using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using DAL.Models;

namespace Secure_Access.Controllers
{
    public class QRController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QRController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Step 1: Show QR code on PC
        public IActionResult QRLogin()
        {
            var qrToken = Guid.NewGuid().ToString();

            // Save token in DB
            _context.QRTokens.Add(new QRToken
            {
                Token = qrToken,
                Scanned = false,
                CreatedAt = DateTime.UtcNow
            });
            _context.SaveChanges();

            ViewBag.QRToken = qrToken;
            return View();
        }

        // Step 2: Generate QR code image
        public IActionResult GenerateQRCode(string token)
        {
            var url = Url.Action("Scan", "QR", new { token = token }, Request.Scheme);

            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrData);
            using var bitmap = qrCode.GetGraphic(20);
            using var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            return File(ms.ToArray(), "image/png");
        }

        // Step 3: Phone scans QR
        public IActionResult Scan(string token)
        {
            var qr = _context.QRTokens.FirstOrDefault(t => t.Token == token);
            if (qr != null)
            {
                qr.Scanned = true;
                _context.SaveChanges();
                return Content("Scan successful! You can close the phone browser.");
            }
            return Content("Invalid QR token.");
        }

        // Step 4: PC polls server to check scan
        public IActionResult CheckScan(string token)
        {
            var qr = _context.QRTokens.FirstOrDefault(t => t.Token == token);
            bool scanned = qr != null && qr.Scanned;
            return Json(new { scanned });
        }
    }
}
