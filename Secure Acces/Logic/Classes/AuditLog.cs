using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Classes
{
    public class AuditLog
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public int UserId { get; private set; }
        public int? DoorId { get; private set; }
        public AuditType AuditType { get; private set; }
        public string? ExtraData { get; private set; }

        public AuditLog(int id, DateTime date, int userId, int? doorId, AuditType type, string? extraData)
        {
            Id = id;
            Date = date;
            UserId = userId;
            DoorId = doorId;
            AuditType = type;
            ExtraData = extraData;
        }

        public static AuditLog CreateDoorOpenRequest(int userId, int doorId)
        {
            return new AuditLog(0, DateTime.Now, userId, doorId, AuditType.DoorOpenRequest, null);
        }

        public static AuditLog CreateDoorAccessGranted(int userId, int doorId, string extraData)
        {
            return new AuditLog(0, DateTime.Now, userId, doorId, AuditType.DoorAccessGranted, extraData);
        }

        public static AuditLog CreateDoorAccessDenied(int userId, int doorId, string reason)
        {
            return new AuditLog(0, DateTime.Now, userId, doorId, AuditType.DoorAccessDenied,
                $"{{ \"reason\": \"{reason}\" }}");
        }

        public static AuditLog CreateQrCodeRequest(int userId, string qrValue)
        {
            return new AuditLog(0, DateTime.Now, userId, null, AuditType.QrCodeRequest,
                $"{{ \"qr\": \"{qrValue}\" }}");
        }

        public static AuditLog CreateLoginAttempt(int userId, bool success, string ip)
        {
            return new AuditLog(0, DateTime.Now, userId, null, AuditType.LoginAttempt,
                $"{{ \"success\": {success.ToString().ToLower()}, \"ip\": \"{ip}\" }}");
        }

        public string GetDescription()
        {
            return AuditType switch
            {
                AuditType.DoorOpenRequest => $"User {UserId} requested door {DoorId}",
                AuditType.DoorAccessGranted => $"User {UserId} was granted access to door {DoorId}",
                AuditType.DoorAccessDenied => $"User {UserId} denied access to door {DoorId}",
                AuditType.QrCodeRequest => $"User {UserId} requested QR code: {ExtraData}",
                AuditType.LoginAttempt => $"User {UserId} login attempt: {ExtraData}",
                _ => "Unknown event"
            };
        }
    }
}
