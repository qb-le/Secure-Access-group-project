using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class QRToken
    {
        [Key]
        public string Token { get; set; } = null!; 

        public bool Scanned { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
