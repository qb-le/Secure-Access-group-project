using Logic.Classes;
using Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interface
{
    public interface IAuditLogRepository
    {
        List<DtoAuditLog> GetAllAuditLogs();
        List<DtoAuditLog> GetAuditLogsByDoorId(int doorId);
        List<DtoAuditLog> GetAuditLogsByUserId(int userId);
        void InsertAuditLog(DtoAuditLog log);
    }
}
