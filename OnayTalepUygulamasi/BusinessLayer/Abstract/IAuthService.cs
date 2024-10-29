using BusinessLayer.Dtos;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        Task<User> Register(SignupRequest signupRequest);
        Task<string> Login(AuthenticationRequest authenticationRequest  );
        string CreateToken(User user);
    }
}
