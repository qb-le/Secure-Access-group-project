using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Classes
{
    public class DoorGroup
    {
        private int _id;
        private string _name;
        private List<Door> doors;

        public DoorGroup(int id, string name)
        {
            _id = id; 
            _name = name;
        }

        public DoorGroup(int id, string name, List<Door> doors) : this(id, name) 
        {
            this.doors = doors;
        }

        public List<Door> GetDoors()
        {
            return doors;
        }

        public int getId()
        {
            return _id;
        }

        public string getName()
        {
            return _name;
        }
    }
}
