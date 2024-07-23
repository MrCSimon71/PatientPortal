using PDDS.PatientData.Core.Entities;

namespace PDDS.PatientData.Core.Repositories
{
    public interface IUserRepository<User, TId> : IBaseRepository<User, TId>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
