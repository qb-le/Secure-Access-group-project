using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Logic.Classes;
using Logic.Dto;
using Logic.Interface;

namespace Logic.Service
{
    public static class AuditLogMapper
    {
        public static AuditLog ToRichModel(DtoAuditLog dto, IDoorRepository doorRepo)
        {
            return new AuditLog(
                id: dto.Id,
                date: dto.Date,
                userId: dto.UserId,
                doorId: dto.DoorId,
                type: dto.AuditType,
                extraData: dto.ExtraData
                );
        }

        public static List<AuditLog> ToRichModels(List<DtoAuditLog> dtoLogs, IDoorRepository doorRepo)
        {
            return dtoLogs.Select(dto => ToRichModel(dto, doorRepo)).ToList();
        }

        public static DtoAuditLog ToDto(AuditLog log, IUserRepository userRepo, IDoorRepository doorRepo)
        {
            string? userName = userRepo.GetUserById(log.UserId);

            string? doorName = null;
            if (log.DoorId.HasValue)
            {
                var door = doorRepo.GetDoorById(log.DoorId.Value);
                doorName = door?.getName();
            }

            return new DtoAuditLog
            {
                Id = log.Id,
                UserId = log.UserId,
                DoorId = log.DoorId,
                Date = log.Date,
                AuditType = log.AuditType,
                ExtraData = log.ExtraData,
                UserName = userName,
                DoorName = doorName
            };
        }

        public static List<DtoAuditLog> ToDtos(List<AuditLog> logs, IUserRepository userRepo, IDoorRepository doorRepo)
        {
            return logs.Select(model => ToDto(model, userRepo, doorRepo)).ToList();
        }

    }
}
