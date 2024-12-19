namespace WebApp.Models.HospitalData
{
    public class Doctor
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Specialization { get; set; }

        public required string ContactNumber { get; set; }

        public required string Email { get; set; }
    }
}
