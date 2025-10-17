using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string? Coordinates { get; set; }

        public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
        public virtual ICollection<Door> Doors { get; set; } = new List<Door>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
