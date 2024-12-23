using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Authentication")]
    public class LoginController : ControllerBase


    {
        private readonly JwtSettings _jwtSettings;

        public LoginController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("login")]

        public IActionResult Login([FromBody] LoginModel loginRequest)
        {
            if (loginRequest.Username != "Suryansh" || loginRequest.Password != "Suryansh")
            {
                return Unauthorized();
            }

            var Token = GenerateJwtToken(loginRequest.Username);

            return Ok(new
            {
                Token,
                loginRequest.Username,
                loginRequest.Password
            });
        }

        private string GenerateJwtToken(string username)
        {
            var claims = Array.Empty<object>();

            {
                new Claim(ClaimTypes.Name, "suryansh@gmail.com");
                new Claim(ClaimTypes.Role, "Admin");
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
