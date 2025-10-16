using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Table("Access_Method")]
public partial class AccessMethod
{
    [Key]
    [Column("access_method_id")]
    public int AccessMethodId { get; set; }

    [Column("name")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Name { get; set; }

    [InverseProperty("AccessMethod")]
    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}
