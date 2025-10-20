using Logic.Classes;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDoorRepository
    {
        List<DoorGroup> GetAllDoorGroups();
        List<Door> GetDoorsByGroupId(int groupId);
        Door? GetDoorById(int id);
    }
}