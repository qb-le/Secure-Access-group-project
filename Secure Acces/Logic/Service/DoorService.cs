using DAL.Interfaces;
using Logic.Classes;
using Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service
{
    public class DoorService : IDoorService
    {
        private readonly IDoorRepository _doorRepository;

        public DoorService(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public List<DoorGroup> GetAllDoorGroups()
        {
            return _doorRepository.GetAllDoorGroups();
        }

        public List<Door> GetDoorsByGroupId(int groupId)
        {
            return _doorRepository.GetDoorsByGroupId(groupId);
        }

        public Door? GetDoorById(int id)
        {
            return _doorRepository.GetDoorById(id);
        }
    }
}
