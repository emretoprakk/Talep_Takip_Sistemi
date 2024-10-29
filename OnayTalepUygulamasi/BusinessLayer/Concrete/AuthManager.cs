using BusinessLayer.Abstract;
using BusinessLayer.Dtos;
using EntityLayer.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly string TOKEN_KEY;

        public AuthManager(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            TOKEN_KEY = _configuration["AppSettings:Token"];
        }

        public async Task<User> Register(SignupRequest signupRequest)
        {
            var existingUser = await _userService.GetUserByEmail(signupRequest.Email);
            if (existingUser != null)
            {
                throw new Exception("Email zaten kayıtlı.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(signupRequest.Password);

            var user = new User
            {
                Username = signupRequest.Username,
                Email = signupRequest.Email,
                Password = passwordHash,
                Role = Role.User
            };

            await _userService.AddUser(user);

            return user;
        }

        public async Task<string> Login(AuthenticationRequest authenticationRequest)
        {
            var user = await _userService.GetUserByEmail(authenticationRequest.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(authenticationRequest.Password, user.Password))
            {
                throw new Exception("Kullanıcı bulunamadı veya yanlış şifre.");
            }

            string token = CreateToken(user);

            return token;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TOKEN_KEY));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
