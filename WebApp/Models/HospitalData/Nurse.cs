namespace WebApp.Models.HospitalData
{
    public class Nurse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string ContactNumber { get; set; }
    }
}
