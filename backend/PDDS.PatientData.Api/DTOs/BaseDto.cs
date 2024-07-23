using Newtonsoft.Json;

namespace PDDS.PatientData.Api.DTOs
{
    public class BaseDto
    {
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int? LastModifiedBy { get; set; }
        public bool Active { get; set; } = true;
    }
}
