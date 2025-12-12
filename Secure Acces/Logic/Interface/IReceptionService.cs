using Logic.Classes;
    
namespace Logic.Interface;

public interface IReceptionService
{
    public Task AddRequestAsync(Request request);
    public List<Request> GetAllRequests();
    public void UpdateRequestStatus(int requestId, int status);
    public Request GetRequestById(int requestId);
    public Request GetLatestRequest();
}