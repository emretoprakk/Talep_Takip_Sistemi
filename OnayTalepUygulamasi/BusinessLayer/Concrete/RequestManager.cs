using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class RequestManager : IRequestService
    {
        private readonly IRequestDal _requestDal;

        public RequestManager(IRequestDal requestDal)
        {
            _requestDal = requestDal;
        }

        public async Task AddRequest(Request request)
        {
            await _requestDal.Add(request);
        }

        public async Task<IEnumerable<Request>> GetAllRequests()
        {
            return await _requestDal.GetAll();
        }

        public async Task<Request> GetRequestById(int id)
        {
            return await _requestDal.Get(id);
        }

        public async Task UpdateRequest(Request request)
        {
            await _requestDal.Update(request);
        }
    }
}