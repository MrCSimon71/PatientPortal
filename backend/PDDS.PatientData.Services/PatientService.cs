using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Filters;
using PDDS.PatientData.Core.Repositories;
using PDDS.PatientData.Core.Services;

namespace PDDS.PatientData.Services
{
    public class PatientService : IPatientService<Patient, int>
    {
        private readonly IPatientRepository<Patient, int> _patientRepository;

        public PatientService(IPatientRepository<Patient, int> patientRepository)
        {
            this._patientRepository = patientRepository;
        }

        public Task<IEnumerable<Patient>> GetAllAsync()
        {
            return _patientRepository.GetAllAsync();
        }

        public async Task<(IEnumerable<Patient> Data, int TotalRecords)> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            var patients = await _patientRepository.GetAllAsync<T>(searchFilter);

            var totalRecords = _patientRepository.GetCountAsync<T>(searchFilter).Result;

            return (patients, totalRecords);
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _patientRepository.GetByIdAsync(id);
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            patient.CreatedOn = DateTime.Now;
            patient.LastModifiedOn = DateTime.Now;

            return await _patientRepository.AddAsync(patient);
        }

        public async Task<bool> UpdateAsync(Patient patient)
        {
            return await _patientRepository.UpdateAsync(patient);
        }

        public async Task<bool> DeleteAsync(int id, int modifiedBy)
        {
            return await _patientRepository.DeleteAsync(id, modifiedBy);
        }
    }
}
