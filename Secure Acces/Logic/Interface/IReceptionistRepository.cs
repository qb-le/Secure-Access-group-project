using Logic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interface
{
    public interface IReceptionistRepository
    {
        public List<Request> GetAllRequests();
        public void AddRequest(Request request);
        public void UpdateRequestStatus(int requestId, int status);
        public Request GetRequestById(int id);
        public Request GetLatestRequest();
    }
}
