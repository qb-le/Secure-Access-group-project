namespace Logic.Classes;

public class Request
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Door { get; set; }
    public DateTime RequestTime { get; set; }
    public string DoorGroup  {get; set; }
    public string Status { get; set; } = "Pending";
}