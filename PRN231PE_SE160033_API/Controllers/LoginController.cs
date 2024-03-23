using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PRN231PE_SE160033_API.Models;
using PRN231PE_SE160033_Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PRN231PE_SE160033_API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IConfiguration _config;
        public LoginController(IAccountRepo accountRepo, IConfiguration config)
        {
            _accountRepo = accountRepo;
            _config = config;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _accountRepo.Get(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
{
    new Claim(ClaimTypes.Role, user.Role.ToString())
};
            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            return Ok(new LoginResponse
            {
                Token = token,
            });
        }
    }
}
