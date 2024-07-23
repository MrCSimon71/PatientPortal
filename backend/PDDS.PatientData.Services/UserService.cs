using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Helpers;
using PDDS.PatientData.Core.Filters;
using PDDS.PatientData.Core.Repositories;
using PDDS.PatientData.Core.Services;

namespace PDDS.PatientData.Services
{
    public class UserService : IUserService<User, int>
    {
        private readonly IUserRepository<User, int> _userRepository;

        public UserService(IUserRepository<User, int> userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<(IEnumerable<User> Data, int TotalRecords)> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            var users = await _userRepository.GetAllAsync<T>(searchFilter);

            var totalRecords = _userRepository.GetCountAsync<T>(searchFilter).Result;

            return (users, totalRecords);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> AddAsync(User user)
        {
            if (user.Password == null)
            {
                var randonPassword = PasswordHelper.GenerateRandomPassword();
                var randomPasswordHash = PasswordHelper.EncryptPassword(randonPassword);
                user.Password = randomPasswordHash;
            }
            else
            {
                user.Password = PasswordHelper.EncryptPassword(user.Password);
            }

            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(int id, int modifiedBy)
        {
            return await _userRepository.DeleteAsync(id, modifiedBy);
        }
    }
}
