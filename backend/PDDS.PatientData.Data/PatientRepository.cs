using Microsoft.EntityFrameworkCore;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Repositories;
using PDDS.PatientData.Core.Helpers;
using PDDS.PatientData.Core.Filters;

namespace PDDS.PatientData.Data
{
    public class PatientRepository : IPatientRepository<Patient, int>
    {
        private readonly PatientDataContext _dbContext;

        public PatientRepository(PatientDataContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            try
            {
                var patients = await _dbContext.Patients
                    .Where(p => !p.Deleted)
                    .ToListAsync();

                return patients;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Patient>> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            try
            {
                var patients = new List<Patient>();

                var filter = (SearchFilter)Convert.ChangeType(searchFilter, typeof(SearchFilter));

                var whereClause = ExpressionBuilder.BuildExpression<Patient>(filter.SearchCriteria);

                if (whereClause != null)
                {
                    patients = await _dbContext.Patients
                        .Where(whereClause)
                        .Where(p => p.Deleted == false)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                else
                {
                    patients = await _dbContext.Patients
                        .Where(p => p.Deleted == false)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }

                return patients;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            try
            {
                var patient = new Patient();

                patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.PatientID == id);

                return patient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            try
            {
                _dbContext.Add(patient);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
            
            return patient;
        }

        public async Task<bool> UpdateAsync(Patient patient)
        {
            try
            {
                var dbEntry = await GetByIdAsync(patient.PatientID);

                if (dbEntry == null)
                    throw new Exception("Update failed. Entity not found");

                _dbContext.Patients.Attach(dbEntry);

                var newValues = new Dictionary<string, object>
                {
                    { "FirstName", patient.FirstName },
                    { "LastName", patient.LastName},
                    { "Address1", patient.Address1 },
                    { "Address2", patient.Address2 },
                    { "City", patient.City },
                    { "State", patient.State },
                    { "PostalCode", patient.PostalCode },
                    { "PrimaryPhone", patient.PrimaryPhone },
                    { "Email", patient.Email },
                    { "DOB", patient.DateOfBirth},
                    { "LastModifiedOn", DateTime.Now },
                    { "LastModifiedBy", patient.LastModifiedBy },
                };

                _dbContext.Entry(dbEntry).CurrentValues.SetValues(newValues);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int id, int modifiedBy)
        {
            try
            {
                var dbEntry = await GetByIdAsync(id);

                if (dbEntry == null)
                    throw new Exception("Delete failed. Entity not found");

                _dbContext.Patients.Attach(dbEntry);

                var newValues = new Dictionary<string, object>
                {
                    { "Active", false },
                    { "Deleted", true },
                    { "LastModifiedOn", DateTime.Now },
                    { "LastModifiedBy", modifiedBy },
                };

                _dbContext.Entry(dbEntry).CurrentValues.SetValues(newValues);

                await _dbContext.SaveChangesAsync();
            }
            catch { throw; }

            return true;
        }

        public async Task<int> GetCountAsync() => await _dbContext.Patients.CountAsync();

        public async Task<int> GetCountAsync<T>(T searchFilter) where T : SearchFilter
        {
            var filter = (SearchFilter)Convert.ChangeType(searchFilter, typeof(SearchFilter));

            var whereClause = ExpressionBuilder.BuildExpression<Patient>(filter.SearchCriteria);

            if (whereClause != null)
            {
                return await _dbContext.Patients
                    .Where(whereClause)
                    .Where(p => p.Deleted == false)
                    .CountAsync();
            }
            else
            {
                return await _dbContext.Patients
                    .Where(p => p.Deleted == false)
                    .CountAsync();
            }
        }
    }
}
