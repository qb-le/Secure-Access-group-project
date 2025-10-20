using Logic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interface
{
    public interface IDoorService
    {
        List<DoorGroup> GetAllDoorGroups();
        List<Door> GetDoorsByGroupId(int groupId);
        Door? GetDoorById(int id);
    }
}
