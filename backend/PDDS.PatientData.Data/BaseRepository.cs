using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Filters;
using PDDS.PatientData.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PDDS.PatientData.Data
{
    public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId> where TEntity : class
    {
        protected PatientDataContext DBContext { get; set; }

        public BaseRepository(PatientDataContext dbContext)
        {
            this.DBContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await this.DBContext.Set<TEntity>().AsNoTracking().ToListAsync<TEntity>();
            }
            catch { throw; }
        }

        public Task<IEnumerable<TEntity>> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            try
            {
                return await this.DBContext.Set<TEntity>().FindAsync(id);
            }
            catch { throw; }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(TId id, int modifiedBy)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync() => await this.DBContext.Set<TEntity>().CountAsync();

        public Task<int> GetCountAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
        }
    }
}
