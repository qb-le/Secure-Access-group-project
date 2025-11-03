using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Classes
{
    public class AuditLog
    {
        private int _id;
        private Door _door;
        private DateTime _date;
        private int _userId;

        public AuditLog(int id, Door door, DateTime date, int userId)
        {
            _id = id;
            _door = door;
            _date = date;
            _userId = userId;
        }

        public int getId() 
        { 
            return _id; 
        }

        public Door getDoor()
        {
            return _door;
        }

        public DateTime getDate()
        {
            return _date;
        }

        public int getUserId()
        {
            return _userId;
        }
    }
}
