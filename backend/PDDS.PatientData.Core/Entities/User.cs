﻿
namespace PDDS.PatientData.Core.Entities;

public partial class User : BaseEntity
{
    public int UserID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
