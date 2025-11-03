using Logic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interface
{
    public interface IAuditLogService
    {
        List<AuditLog> GetAllAuditLogs();
        List<AuditLog> GetAuditLogsByDoorId(int doorId);
        List<AuditLog> GetAuditLogsByUserId(int userId);

    }
}
