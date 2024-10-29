using BusinessLayer.Dtos;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRequestService
    {
        Task AddRequest(Request request);
        Task<IEnumerable<Request>> GetAllRequests();
        Task<Request> GetRequestById(int id);
        Task UpdateRequest(Request request);
    }
}
