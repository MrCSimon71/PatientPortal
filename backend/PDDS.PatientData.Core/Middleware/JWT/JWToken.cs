using System;
using System.Collections.Generic;
using System.Text;

namespace PDDS.PatientData.Core.Middleware.JWT
{
    public class JWToken
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
