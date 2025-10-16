using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Table("Door")]
public partial class Door
{
    [Key]
    [Column("door_id")]
    public int DoorId { get; set; }

    [Column("number")]
    public double? Number { get; set; }

    [Column("location_id")]
    public int? LocationId { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("Doors")]
    public virtual Location? Location { get; set; }
}
