using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Controllers;

namespace WebApp.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class LoginController : ControllerBase
    {

        private static readonly List<LoginModel> LoginModel = new List<LoginModel>
        {
            new LoginModel {Username="admin",Password="admin"}
        };

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginRequest)
        {
            var user = LoginModel.FirstOrDefault(u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);

            if(user == null)
            {
                return Unauthorized(new
                {
                    Message = "Invalid username or Password"
                });
            }

            var token = JWTHelperClass.GenerateToken(user.Username);

            return Ok(new
            {
                Token = token,
                Message = "Login Successfull",
                Timestamp = DateTime.UtcNow
            });
        }
    }
}
