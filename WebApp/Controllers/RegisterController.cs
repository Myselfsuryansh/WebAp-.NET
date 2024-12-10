using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private static readonly List<RegisterModel> registerModels = new List<RegisterModel>();

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel registerRequest)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registerRequest.UserName) || string.IsNullOrWhiteSpace(registerRequest.Password))
                {
                    return BadRequest(
                        new
                        {
                            Message = "Username and Password are required.",
                            Timestamp = DateTime.UtcNow,
                        }
                        );
                }

                if (registerModels.Any(u => u.Email.Equals(registerRequest.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    return Conflict(new
                    {
                        Message = "Email already exist",
                        Timestamp = DateTime.UtcNow,
                    });
                }

                var newUser = new RegisterModel
                {
                    UserName = registerRequest.UserName,
                    Email = registerRequest.Email,
                    Password = registerRequest.Password,
                };
                registerModels.Add(newUser);

                return CreatedAtAction(nameof(Register), new
                {
                    Message = "User Registered Successfully",
                    Timestamp = DateTime.UtcNow,
                    UserId = newUser.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occured while fetching the userData",
                    ErrorDetails = ex,
                    TimeStamp = DateTime.UtcNow
                });

            }


        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] RegisterModel loginRequest)
        {
            var user = registerModels.FirstOrDefault(u=> u.UserName == loginRequest.UserName && u.Password == loginRequest.Password);
            if (user == null)
            {
                return Unauthorized(new
                {
                    Message = "Invalid useraname or password"
                });
            }

            var token = JWTHelperClass.GenerateToken(user.UserName);

            return Ok(new
            {
                Token = token,
                Message = "Login Successfull",
                Timestamp = DateTime.UtcNow
            });


        }
    }
}
