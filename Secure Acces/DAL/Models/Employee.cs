using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DAL.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public int? RoleId { get; set; }
        public int? LocationId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Location? Location { get; set; }
    }
}
