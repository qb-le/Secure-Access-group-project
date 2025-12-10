using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Classes
{
    public enum AuditType
    {
        DoorOpenRequest = 0,
        DoorAccessGranted = 1,
        DoorAccessDenied = 2,
        QrCodeRequest = 3,
        LoginAttempt = 4
    }
}
