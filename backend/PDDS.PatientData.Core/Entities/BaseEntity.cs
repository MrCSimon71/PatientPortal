
namespace PDDS.PatientData.Core.Entities;    

public partial class BaseEntity
{
    public DateTime? CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public int LastModifiedBy { get; set; }
    public bool Active { get; set; } = true;
    public bool Deleted { get; set; } = false;
}
