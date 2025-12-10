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
            Door? door = null;
            if (dto.DoorId.HasValue)
            {
                door = doorRepo.GetDoorById(dto.DoorId.Value);
            }

            return new AuditLog(
                id: dto.Id,
                date: dto.Date,
                userId: dto.UserId,
                door: door,
                type: dto.AuditType,
                extraData: dto.ExtraData
                );
        }

        public static List<AuditLog> ToRichModels(List<DtoAuditLog> dtoLogs, IDoorRepository doorRepo)
        {
            return dtoLogs.Select(dto => ToRichModel(dto, doorRepo)).ToList();
        }

        public static DtoAuditLog ToDto(AuditLog log, IUserRepository userRepo)
        {
            string? userName = userRepo.GetUserById(log.UserId);

            return new DtoAuditLog
            {
                Id = log.Id,
                UserId = log.UserId,
                DoorId = log.Door?.getId(),
                Date = log.Date,
                AuditType = log.AuditType,
                ExtraData = log.ExtraData,
                UserName = userName,
                DoorName = log.Door?.getName()
            };
        }

        public static List<DtoAuditLog> ToDtos(List<AuditLog> logs, IUserRepository userRepo)
        {
            return logs.Select(model => ToDto(model, userRepo)).ToList();
        }

    }
}
