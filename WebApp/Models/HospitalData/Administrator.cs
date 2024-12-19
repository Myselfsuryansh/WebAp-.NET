namespace WebApp.Models.HospitalData
{
    public class Administrator
    {

        public Guid Id { get; set; }

        public required String Name {  get; set; }

        public required string ContactName { get; set; }

        public required string Email { get; set; }
    }
}
