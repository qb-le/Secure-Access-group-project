using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Classes
{
    public enum AuditType
    {
        DoorOpenRequest = 1,
        DoorAccessGranted = 2,
        DoorAccessDenied = 3,
        QrCodeRequest = 4,
        LoginAttempt = 5
    }
}
