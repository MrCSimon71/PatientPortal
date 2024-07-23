using Moq;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Repositories;
using PDDS.PatientData.Core.Services;
using PDDS.PatientData.Services;

namespace PDDS.PatientData.Api.Tests
{
    public class PatientServiceTests
    {
        [Fact]
        public void GetByIdAsync_ReturnsPatient_WhenPatientIdIsValid()
        {
            var patientID = 5;
            var mockPatientRepo = new Mock<IPatientRepository<Patient, int>>();

            mockPatientRepo
                .Setup(mock => mock.GetByIdAsync(patientID))
                .Returns(Task.FromResult<Patient>(new Patient()
                {
                    PatientID = patientID,
                    FirstName = "Sally",
                    LastName = "Jesse",
                    DateOfBirth = DateTime.Now,
                    PrimaryPhone = "5552123455"
                }));

            var patientSvc = new PatientService(mockPatientRepo.Object);
            var result = patientSvc.GetByIdAsync(patientID);

            Assert.True(result.Result != null && result.Result.PatientID == patientID);
        }

        [Fact]
        public void GetByIdAsync_ReturnsNull_WhenPatientIdIsInvalid()
        {
            var patientID = 5;
            var mockPatientRepo = new Mock<IPatientRepository<Patient, int>>();

            mockPatientRepo
                .Setup(mock => mock.GetByIdAsync(patientID))
                .Returns(Task.FromResult<Patient>(null));

            var patientSvc = new PatientService(mockPatientRepo.Object);
            var result = patientSvc.GetByIdAsync(patientID);

            Assert.True(result.Result == null);
        }
    }
}
