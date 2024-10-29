    using BusinessLayer.Abstract;
using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace OnayTalepUygulamasi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(SignupRequest signupRequest)
        {
            try
            {
                var user = await _authService.Register(signupRequest);
                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticationRequest authenticationRequest)
        {
            try
            {
                var token = await _authService.Login(authenticationRequest);
                var user = await _userService.GetUserByEmail(authenticationRequest.Email);
                return Ok(new { token, userId = user.Id });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("user-info")]
        public async Task<ActionResult<UserDto>> GetUserInfo()
        {
            try
            {
                var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token == null)
                    return Unauthorized("Token bulunamadı");

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return Unauthorized("Geçersiz token");

                var usernameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

                if (usernameClaim == null)
                    return Unauthorized("Geçersiz token");

                var user = await _userService.GetUserByUsername(usernameClaim.Value);

                if (user == null)
                    return NotFound("Kullanıcı bulunamadı");

                var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

                var userInfo = new UserDto
                {
                    Username = usernameClaim.Value,
                    Email = emailClaim.Value,
                    Role = user.Role,
                };

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata meydana geldi: {ex.Message}");
            }
        }


    }
}

