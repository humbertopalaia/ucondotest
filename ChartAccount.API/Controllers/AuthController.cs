using ChartAccount.API.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChartAccount.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("GetToken")]
        [AllowAnonymous]
        public IActionResult GetToken([FromBody] GetTokenModel model)
        {
            string token = string.Empty;

            try
            {
                var username = _configuration["Jwt:Username"];
                var password = _configuration["Jwt:Password"];

                if(username == model.Username && password == model.Password)
                {
                    token = GenerateToken(model.Username);
                }
                else
                    return StatusCode(401, "Invalid credentials");
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Internal error");
                return StatusCode(500, "Internal error");
            }

            return Json(new { Success = true, Token = token });
        }

        private string GenerateToken(string username)
        {

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Email, username),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}
