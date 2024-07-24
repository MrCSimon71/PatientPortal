namespace PDDS.PatientData.Api.DTOs
{
    public class PatientDto : BaseDto
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
        public string PrimaryPhone { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
