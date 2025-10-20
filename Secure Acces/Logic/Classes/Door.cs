using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Classes
{
    public class Door
    {
        private int _id;
        private string _name;
        private int _groupId;

        public Door(int id, string name, int groupId)
        {
            _id = id;
            _name = name;
            _groupId = groupId;
        }

        public int getId()
        {
            return _id;
        }

        public string getName()
        {
            return _name;
        }

        public int getGroupId()
        {
            return _groupId;
        }
    }
}
