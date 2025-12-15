using Logic.Classes;
using Logic.Interface;
using Microsoft.AspNetCore.SignalR;

namespace Logic.Service;

public class ReceptionService : IReceptionService
{
    private readonly IReceptionistRepository _repository;
    private readonly IHubContext<AccessHub> _hubContext;

    public ReceptionService(IReceptionistRepository repository, IHubContext<AccessHub> hubContext)
    {
        _repository = repository;
        _hubContext = hubContext;
    }

    public async Task AddRequestAsync(Request request)
    {
        _repository.AddRequest(request);
    }

    public List<Request> GetAllRequests()
    {
       return _repository.GetAllRequests();
    }

    public void UpdateRequestStatus(int requestId, int status)
    {
        _repository.UpdateRequestStatus(requestId, status);
    }

    public Request GetRequestById(int id)
    {
        return _repository.GetRequestById(id);
    }

    public Request GetLatestRequest()
    {
        return _repository.GetLatestRequest();
    }
}
