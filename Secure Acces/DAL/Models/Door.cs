using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Door
    {
        [Key]
        public int DoorId { get; set; }
        public double? Number { get; set; }
        public int? LocationId { get; set; }

        public virtual Location? Location { get; set; }
    }
}
