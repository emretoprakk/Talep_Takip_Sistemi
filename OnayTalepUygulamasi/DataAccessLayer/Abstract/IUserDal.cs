using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUserDal : IGenericDal<User>
    {
        Task<User> GetUserByUsername(string username);
        Task<IEnumerable<User>> GetUsersByRole(Role role);
        Task<User> GetUserByEmail(string email);
    }
}
