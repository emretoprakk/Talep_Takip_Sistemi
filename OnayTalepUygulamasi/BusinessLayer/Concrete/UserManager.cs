using BusinessLayer.Abstract;
using BusinessLayer.Dtos;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task AddUser(User user)
        {
            await _userDal.Add(user);
        }

        public async Task UpdateUser(User user)
        {
            await _userDal.Update(user);
        }

        public async Task DeleteUser(User user)
        {
            await _userDal.Delete(user);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userDal.Get(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userDal.GetAll();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _userDal.GetUserByUsername(username);
        }

        public async Task<IEnumerable<User>> GetUsersByRole(Role role)
        {
            return await _userDal.GetUsersByRole(role);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userDal.GetUserByEmail(email);
        }
    }
}
