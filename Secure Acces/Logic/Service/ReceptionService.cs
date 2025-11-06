using Logic.Classes;
using Logic.Interface;

namespace Logic.Service;

public class ReceptionService : IReceptionService
{
    private static List<Request> requests = new List<Request>
    {
        new Request { Id = 1, Name = "Hanna Thomas", DoorGroup = "AirBNB", Door = "Front Entrance" },
        new Request { Id = 2, Name = "Micheal Leon", DoorGroup = "Office", Door = "Staff Room" },
    };

    public List<Request> GetAllRequests()
    {
        return requests;
    }

    public void GrantAccess(int id)
    {
        var req = requests.FirstOrDefault(r => r.Id == id);
        if (req != null)
            req.Status = "Granted";
    }

    public void RejectAccess(int id)
    {
        var req = requests.FirstOrDefault(r => r.Id == id);
        if (req != null)
            req.Status = "Rejected";
    }
}
