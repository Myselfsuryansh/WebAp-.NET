namespace WebApp.Models.HospitalData
{
    public class Patient
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public required string Gender { get; set; }

        public required string PhoneNumber { get; set; }

        public required string Address { get; set; }

        public DateTime AdmissionDate { get; set; }



    }
}
