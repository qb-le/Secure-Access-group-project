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
            var richLogs = GetAllLogsRich();
            return AuditLogMapper.ToDtos(richLogs, _userRepo, _doorRepo);
        }

        public List<DtoAuditLog> GetLogsByUserDto(int userId)
        {
            var richLogs = GetLogsByUserRich(userId);
            return AuditLogMapper.ToDtos(richLogs, _userRepo, _doorRepo);
        }

        public List<DtoAuditLog> GetLogsByDoorDto(int doorId)
        {
            var richLogs = GetLogsByDoorRich(doorId);
            return AuditLogMapper.ToDtos(richLogs, _userRepo, _doorRepo);
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

        public void LogDoorOpenRequest(DtoAuditLog dto)
        {
            var log = AuditLog.CreateDoorOpenRequest(dto.UserId, dto.DoorId ?? 0);
            SaveLog(log);
        }

        public void LogDoorAccessGranted(DtoAuditLog dto)
        {
            int receptionistUserId = 0; //get actual receptionist userId from session

            string extraData = $"{{ granted For UserId: {dto.UserId} }}";

            var log = AuditLog.CreateDoorAccessGranted(receptionistUserId, dto.DoorId ?? 0, extraData);
            SaveLog(log);
        }

        public void LogDoorAccessDenied(DtoAuditLog dto)
        {
            int receptionistUserId = 0; //get actual receptionist userId from session

            string extraData = $"{{ denied For UserId: {dto.UserId} }}";

            var log = AuditLog.CreateDoorAccessDenied(receptionistUserId, dto.DoorId ?? 0, extraData);
            SaveLog(log);
        }

        public void LogQrCodeRequest(DtoAuditLog dto)
        {
            var qrValue = dto.ExtraData ?? "";
            var log = AuditLog.CreateQrCodeRequest(dto.UserId, qrValue);
            SaveLog(log);
        }

        public void LogLoginAttempt(DtoAuditLog dto)
        {
            bool success = dto.ExtraData?.Contains("true") ?? false;
            string ip = dto.ExtraData ?? "";

            var log = AuditLog.CreateLoginAttempt(dto.UserId, success, ip);
            SaveLog(log);
        }

        private void SaveLog(AuditLog log)
        {

            var dto = AuditLogMapper.ToDto(log, _userRepo, _doorRepo);
            _logRepo.InsertAuditLog(dto);
        }
    }
}
