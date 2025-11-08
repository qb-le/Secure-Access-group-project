using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Classes
{
    public class QRTokenInfo
    {
        public bool Scanned { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int DoorId { get; set; }
        public int DoorGroupId { get; set; }
    }
}

