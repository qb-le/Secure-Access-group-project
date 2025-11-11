using DAL.Interfaces;
using Logic.Classes;
using Logic.Dto;
using Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service
{
    public class AuditLogService
    {
        private readonly IAuditLogRepository _logRepository;
        private readonly IDoorRepository _doorRepository;

        public AuditLogService(IAuditLogRepository logRepository, IDoorRepository doorRepository)
        {
            _logRepository = logRepository;
            _doorRepository = doorRepository;

        }

        public List<AuditLog> GetAllLogs()
        {
            var dtoLogs = _logRepository.GetAllAuditLogs();
            var doors = _doorRepository.GetAllDoors();

            var logs = new List<AuditLog>();

            foreach (var dto in dtoLogs)
            {
                var door = doors.FirstOrDefault(d => d.getId() == dto.DoorId);

                if (door != null)
                {
                    logs.Add(new AuditLog(dto.Id, door, dto.Date, dto.UserId));
                }
            }

            return logs;
        }


        public void AddLogDoorAccess(int userId, int doorId)
        {
            var newlog = new DtoAuditLog()
            {
                UserId = userId,
                DoorId = doorId
            };

            _logRepository.InsertAuditLog(newlog);
        }
    }
}
