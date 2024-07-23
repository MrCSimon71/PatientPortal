using Microsoft.EntityFrameworkCore;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Repositories;
using PDDS.PatientData.Core.Filters;
using PDDS.PatientData.Core.Helpers;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PDDS.PatientData.Data
{
    public class UserRepository : IUserRepository<User, int>
    {
        private readonly PatientDataContext _dbContext;

        public UserRepository(PatientDataContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Users.ToListAsync();
            }
            catch { throw; }
        }

        public async Task<IEnumerable<User>> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            try
            {
                var users = new List<User>();

                var filter = (SearchFilter)Convert.ChangeType(searchFilter, typeof(SearchFilter));

                var whereClause = ExpressionBuilder.BuildExpression<User>(filter.SearchCriteria);

                if (whereClause != null)
                {
                    users = await _dbContext.Users
                        .Where(whereClause)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                else
                {
                    users = await _dbContext.Users
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }

                return users;
            }
            catch { throw ; }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                var user = new User();
                user = await _dbContext.Users.FirstOrDefaultAsync(p => p.UserID == id);
                return user;
            }
            catch { throw; }
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            try
            {
                var user = new User();
                user = await _dbContext.Users.FirstOrDefaultAsync(p => p.Username == username);
                return user;
            }
            catch { throw; }
        }

        public async Task<User> AddAsync(User user)
        {
            try
            {
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.Users.Add(user);

                await _dbContext.SaveChangesAsync();
            }
            catch { throw; }

            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                var dbEntry = await GetByIdAsync(user.UserID);

                if (dbEntry == null)
                    throw new Exception("Update failed. Entity not found");

                var newValues = new Dictionary<string, object>
                {
                    { "FirstName", user.FirstName },
                    { "LastName", user.LastName },
                    { "Email", user.Email },
                    { "Username", user.Username },
                };

                _dbContext.Entry(dbEntry).CurrentValues.SetValues(newValues);

                await _dbContext.SaveChangesAsync();
            }
            catch { throw; }

            return true;
        }

        public async Task<bool> DeleteAsync(int id, int modifiedBy)
        {
            try
            {
                var dbEntry = await GetByIdAsync(id);

                if (dbEntry == null)
                    throw new Exception("Delete failed. Entity not found");

                var newValues = new Dictionary<string, object>
                {
                    { "Active", false },
                    { "Deleted", true },
                    { "LastModifiedOn", DateTime.Now },
                    { "LastModifiedBy", modifiedBy }
                };

                _dbContext.Entry(dbEntry).CurrentValues.SetValues(newValues);

                await _dbContext.SaveChangesAsync();
            }
            catch { throw; }

            return true;
        }

        public async Task<int> GetCountAsync() => await _dbContext.Users.CountAsync();
        
        public async Task<int> GetCountAsync<T>(T searchFilter) where T : SearchFilter
        {
            var filter = (SearchFilter)Convert.ChangeType(searchFilter, typeof(SearchFilter));

            var whereClause = ExpressionBuilder.BuildExpression<User>(filter.SearchCriteria);

            if (whereClause != null)
            {
                return await _dbContext.Users
                    .Where(whereClause)
                    .CountAsync();
            }
            else
            {
                return await _dbContext.Users.CountAsync();
            }
        }

    }
}
