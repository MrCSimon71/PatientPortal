using PDDS.PatientData.Core.Filters;

namespace PDDS.PatientData.Core.Repositories
{
    public interface IBaseRepository<TEntity, TId>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync<T>(T searchFilter) where T : SearchFilter;
        Task<TEntity> GetByIdAsync(TId id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TId id, int modifiedBy);
        Task<int> GetCountAsync();
        Task<int> GetCountAsync<T>(T searchFilter) where T : SearchFilter;
    }
}
