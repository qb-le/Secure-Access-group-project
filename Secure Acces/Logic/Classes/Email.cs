using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;
using System.IO;

namespace Logic.Classes
{
    public class Email
    {
        private string mailServer = "smtp.gmail.com";
        private int mailPort = 587;
        private string senderName = "Secure Access Application";
        private string senderEmail = "secureaccessapplication@gmail.com";
        private string password = "oqvw xtdf hupz abcz";
        private string subject = "QR Code Access";

        public void SendEmailWithQR(string receiverName, string receiverEmail, string htmlTemplate, byte[] qrImageBytes)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.To.Add(new MailboxAddress(receiverName, receiverEmail));
            message.Subject = subject;

            var builder = new BodyBuilder();

            // Embed QR as inline image
            var image = builder.LinkedResources.Add("qrcode.png", new MemoryStream(qrImageBytes));
            image.ContentId = MimeUtils.GenerateMessageId();

            // Replace placeholder in HTML with the inline image reference
            builder.HtmlBody = htmlTemplate.Replace("{{QRCodeImage}}", $"cid:{image.ContentId}");

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(mailServer, mailPort, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(senderEmail, password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
