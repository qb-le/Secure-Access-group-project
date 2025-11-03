using Logic.Classes;
using Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _repository;

        public AuditLogService(IAuditLogRepository repository) 
        {
            _repository = repository;
        }

        public List<AuditLog> GetAllAuditLogs()
        {
            return _repository.GetAllAuditLogs();
        }

        public List<AuditLog> GetAuditLogsByDoorId(int doorId)
        {
            return _repository.GetAuditLogsByDoorId(doorId);
        }

        public List<AuditLog> GetAuditLogsByUserId(int userId)
        {
            return _repository.GetAuditLogsByUserId(userId);
        }
    }
}
