using DAL.Interfaces;
using Logic.Classes;
using Logic.Dto;
using Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Service
{
    public class AuditLogService
    {
        private readonly IAuditLogRepository _logRepo;
        private readonly IDoorRepository _doorRepo;
        private readonly IUserRepository _userRepo;

        public AuditLogService(IAuditLogRepository logRepo, IDoorRepository doorRepo, IUserRepository userRepo)
        {
            _logRepo = logRepo;
            _doorRepo = doorRepo;
            _userRepo = userRepo;
        }

        public List<DtoAuditLog> GetAllLogsDto()
        {
            return _logRepo.GetAllAuditLogs();
            
        }

        public List<DtoAuditLog> GetLogsByUserDto(int userId)
        {
            return _logRepo.GetAuditLogsByUserId(userId);
        }

        public List<DtoAuditLog> GetLogsByDoorDto(int doorId)
        {
            return _logRepo.GetAuditLogsByDoorId(doorId);
        }

        public List<AuditLog> GetAllLogsRich()
        {
            var dtoLogs = _logRepo.GetAllAuditLogs();
            return AuditLogMapper.ToRichModels(dtoLogs, _doorRepo);
        }

        public List<AuditLog> GetLogsByUserRich(int userId)
        {
            var dtoLogs = _logRepo.GetAuditLogsByUserId(userId);
            return AuditLogMapper.ToRichModels(dtoLogs, _doorRepo);
        }

        public List<AuditLog> GetLogsByDoorRich(int doorId)
        {
            var dtoLogs = _logRepo.GetAuditLogsByDoorId(doorId);
            return AuditLogMapper.ToRichModels(dtoLogs, _doorRepo);
        }

        public void LogDoorOpenRequest(int userId, int doorId)
        {
            var door = _doorRepo.GetDoorById(doorId);
            var log = AuditLog.CreateDoorOpenRequest(userId, door);
            SaveLog(log);
        }

        public void LogDoorAccessGranted(int userId, int doorId)
        {
            var door = _doorRepo.GetDoorById(doorId);
            var log = AuditLog.CreateDoorAccessGranted(userId, door);
            SaveLog(log);
        }

        public void LogDoorAccessDenied(int userId, int doorId, string reason)
        {
            var door = _doorRepo.GetDoorById(doorId);
            var log = AuditLog.CreateDoorAccessDenied(userId, door, reason);
            SaveLog(log);
        }

        public void LogQrCodeRequest(int userId, string qrValue)
        {
            var log = AuditLog.CreateQrCodeRequest(userId, qrValue);
            SaveLog(log);
        }

        public void LogLoginAttempt(int userId, bool success, string ip)
        {
            var log = AuditLog.CreateLoginAttempt(userId, success, ip);
            SaveLog(log);
        }
        
        private void SaveLog(AuditLog log)
        {

            var dto = AuditLogMapper.ToDto(log, _userRepo);
            _logRepo.InsertAuditLog(dto);
        }
    }
}
