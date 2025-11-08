using Logic.Classes;
    
namespace Logic.Interface;

public interface IReceptionService
{
    public Task AddRequestAsync(Request request);
    public List<Request> GetAllRequests();
}