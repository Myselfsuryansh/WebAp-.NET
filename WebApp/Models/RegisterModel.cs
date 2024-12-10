namespace WebApp.Models
{
    public class RegisterModel
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public required string UserName { get; set; }

        public required string Password { get; set; }

        public required string Email { get; set; }
    }
}
