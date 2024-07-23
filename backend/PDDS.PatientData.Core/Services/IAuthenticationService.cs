
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.JWT;

namespace PDDS.PatientData.Core.Services
{
    public interface IAuthenticationService<User, TId> : IBaseService<User, TId>
    {
        Task<JWToken> Authenticate(string username, string password);
    }
}
