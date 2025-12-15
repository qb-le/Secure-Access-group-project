using Logic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Dto
{
    public class DtoAuditLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int? DoorId { get; set; }
        public AuditType AuditType { get; set; }
        public string? ExtraData { get; set; }

        public string? UserName { get; set; }
        public string? DoorName { get; set; }
    }
}
