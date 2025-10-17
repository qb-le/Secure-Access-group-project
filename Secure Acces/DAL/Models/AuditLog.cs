using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class AuditLog
    {
        [Key]
        public int AuditLogId { get; set; }
        public int? EmployeeId { get; set; }
        public double? DoorNumber { get; set; }
        public DateTime? DateTime { get; set; }
        public int? LocationId { get; set; }
        public int? AccessMethodId { get; set; }

        public virtual AccessMethod? AccessMethod { get; set; }
        public virtual Location? Location { get; set; }
    }
}
