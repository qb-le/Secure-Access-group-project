using System.Drawing;

namespace Logic.Classes;

public class Request
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public int DoorId { get; private set; }
    public string Door { get; private set; }
    public DateTime RequestTime { get; private set; }
    public int Status { get; set; } = 2;
    // 0 = Rejected 1 = approved 2 = pending

    public Request(int id, string name, string email, string doorname, DateTime requesttime, int status)
    {
        Id = id;
        Name = name;
        Email = email;
        Door = doorname;
        RequestTime = requesttime;
        Status = status;
    }

    public Request( string name, string email, int doorId, DateTime requestTime, int status)
    {
        Name = name;
        Email = email;
        DoorId = doorId;
        RequestTime = requestTime;
        Status = status;
    }
}