using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Table("Location")]
public partial class Location
{
    [Key]
    [Column("location")]
    public int Location1 { get; set; }

    [Column("coordinates")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Coordinates { get; set; }

    [InverseProperty("Location")]
    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    [InverseProperty("Location")]
    public virtual ICollection<Door> Doors { get; set; } = new List<Door>();

    [InverseProperty("Location")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
