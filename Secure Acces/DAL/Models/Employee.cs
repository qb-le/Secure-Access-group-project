using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Table("Employee")]
public partial class Employee
{
    [Key]
    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("name")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("email")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("password_hash")]
    [StringLength(1)]
    [Unicode(false)]
    public string? PasswordHash { get; set; }

    [Column("role_id")]
    public int? RoleId { get; set; }

    [Column("location_id")]
    public int? LocationId { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("Employees")]
    public virtual Location? Location { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Employees")]
    public virtual Role? Role { get; set; }
}
