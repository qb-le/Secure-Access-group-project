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
}
