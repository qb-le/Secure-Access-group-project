using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Table("Audit_Log")]
public partial class AuditLog
{
    [Key]
    [Column("audit_log_id")]
    public int AuditLogId { get; set; }

    [Column("employee_id")]
    public int? EmployeeId { get; set; }

    [Column("door_number")]
    public double? DoorNumber { get; set; }

    [Column("date_time", TypeName = "datetime")]
    public DateTime? DateTime { get; set; }

    [Column("location_id")]
    public int? LocationId { get; set; }

    [Column("access_method_id")]
    public int? AccessMethodId { get; set; }

    [ForeignKey("AccessMethodId")]
    [InverseProperty("AuditLogs")]
    public virtual AccessMethod? AccessMethod { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("AuditLogs")]
    public virtual Location? Location { get; set; }
}
