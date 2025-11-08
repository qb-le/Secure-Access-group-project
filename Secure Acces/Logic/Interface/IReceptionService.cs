using Logic.Classes;
    
namespace Logic.Interface;

public interface IReceptionService
{
    List<Request> GetAllRequests();
    void GrantAccess(int id);
    void RejectAccess(int id);
}