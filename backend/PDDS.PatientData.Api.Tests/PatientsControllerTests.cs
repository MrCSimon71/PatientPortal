using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using PDDS.PatientData.Api.DTOs;
using PDDS.PatientData.Api.Mappers;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Services;

namespace PDDS.PatientData.Api.Tests
{
    public class PatientsControllerTests
    {
        [Fact]
        public async Task GetById_ReturnsOk_WhenPatientIdIsValid()
        {
            var patientID = 5;
            var mockPatientService = new Mock<IPatientService<Patient, int>>();
            var mockLogger = new Mock<ILogger<PatientsController>>();
            var mockUriService = new Mock<IUriService>();

            var patientProfile = new PatientMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(patientProfile));
            var mockMapper = new Mapper(configuration);

            mockPatientService
                .Setup(mock => mock.GetByIdAsync(patientID))
                .Returns(Task.FromResult<Patient>(new Patient()
                {
                    PatientID = patientID,
                    FirstName = "Sally",
                    LastName = "Jesse",
                    DateOfBirth = DateTime.Now,
                    PrimaryPhone = "5552123455"
                }));

            var controller = new PatientsController(mockPatientService.Object, mockUriService.Object, mockMapper, mockLogger.Object);
            var result = await controller.Get(patientID);

            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode == (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_Returns404NotFound_WhenPatientIdIsInvalid()
        {
            var patientID = 99;
            var mockPatientService = new Mock<IPatientService<Patient, int>>();
            var mockLogger = new Mock<ILogger<PatientsController>>();
            var mockUriService = new Mock<IUriService>();
            var mockMapper = new Mock<IMapper>();

            mockPatientService
                .Setup(mock => mock.GetByIdAsync(patientID))
                .Returns(Task.FromResult<Patient>(null));

            var controller = new PatientsController(mockPatientService.Object, mockUriService.Object, mockMapper.Object, mockLogger.Object);
            var result = await controller.Get(patientID);

            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode == (int)System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_Returns201Created_WhenPatientDtoIsValid()
        {
            var mockPatientService = new Mock<IPatientService<Patient, int>>();
            var mockLogger = new Mock<ILogger<PatientsController>>();
            var mockUriService = new Mock<IUriService>();

            var patientProfile = new PatientMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(patientProfile));
            var mockMapper = new Mapper(configuration);

            mockPatientService
                .Setup(mock => mock.AddAsync(It.IsAny<Patient>()))
                .Returns(Task.FromResult<Patient>(new Patient()
                {
                    PatientID = 8,
                    FirstName = "Salem",
                    LastName = "Williams",
                    PrimaryPhone = "3435648899",
                    DateOfBirth = Convert.ToDateTime("02/12/2000")
                }));

            var controller = new PatientsController(mockPatientService.Object, mockUriService.Object, mockMapper, mockLogger.Object);
            var result = await controller.Post(new PatientDto());

            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode == (int)System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenPatientDtoIsNull()
        {
            var mockPatientService = new Mock<IPatientService<Patient, int>>();
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(It.IsAny<IPatientService<Patient, int>>(), It.IsAny<IUriService>(), It.IsAny<IMapper>(), mockLogger.Object);
            var result = await controller.Post(null);

            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode == (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(It.IsAny<IPatientService<Patient, int>>(), It.IsAny<IUriService>(), It.IsAny<IMapper>(), mockLogger.Object);
            controller.ModelState.AddModelError("test", "test");

            var result = await controller.Post(new PatientDto());

            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode == (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_ReturnsOk_WhenPatientDtoIsValid()
        {
            var patientID = 3;
            var mockPatientService = new Mock<IPatientService<Patient, int>>();
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var patientProfile = new PatientMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(patientProfile));
            var mockMapper = new Mapper(configuration);

            mockPatientService
                .Setup(mock => mock.UpdateAsync(It.IsAny<Patient>()))
                .Returns(Task.FromResult<bool>(true));

            var controller = new PatientsController(mockPatientService.Object, It.IsAny<IUriService>(), mockMapper, mockLogger.Object);
            var result = await controller.Put(patientID, new PatientDto() { PatientID = patientID });

            Assert.True(((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode == (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenPatientDtoIsNull()
        {
            var mockPatientService = new Mock<IPatientService<Patient, int>>();
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(It.IsAny<IPatientService<Patient, int>>(), It.IsAny<IUriService>(), It.IsAny<IMapper>(), mockLogger.Object);
            var result = await controller.Put(It.IsAny<int>(), null);

            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode == (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(It.IsAny<IPatientService<Patient, int>>(), It.IsAny<IUriService>(), It.IsAny<IMapper>(), mockLogger.Object);
            controller.ModelState.AddModelError("test", "test");

            var result = await controller.Put(It.IsAny<int>(), new PatientDto());

            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode == (int)System.Net.HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task Put_ReturnsBadRequest_WhenPatientIdNotMatchingIdFromUri()
        {
            var mockPatientService = new Mock<IPatientService<Patient, int>>();
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(It.IsAny<IPatientService<Patient, int>>(), It.IsAny<IUriService>(), It.IsAny<IMapper>(), mockLogger.Object);
            var result = await controller.Put(It.IsAny<int>(), new PatientDto() { PatientID = 3 });

            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode == (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenPatientIdAndModifiedByAreValid()
        {
            var mockPatientService = new Mock<IPatientService<Patient, int>>();
            var mockLogger = new Mock<ILogger<PatientsController>>();

            mockPatientService
                .Setup(mock => mock.DeleteAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult<bool>(true));

            var controller = new PatientsController(mockPatientService.Object, It.IsAny<IUriService>(), It.IsAny<IMapper>(), mockLogger.Object);
            var result = await controller.Delete(3, 5);

            Assert.True(((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode == (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest_WhenPatientIdFromUriIsInvalid()
        {
            var mockPatientService = new Mock<IPatientService<Patient, int>>();
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(It.IsAny<IPatientService<Patient, int>>(), It.IsAny<IUriService>(), It.IsAny<IMapper>(), mockLogger.Object);
            var result = await controller.Delete(It.IsAny<int>(), It.IsAny<int>());

            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode == (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest_WhenModifiedByFromQueryStringIsInvalid()
        {
            var mockPatientService = new Mock<IPatientService<Patient, int>>();
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(It.IsAny<IPatientService<Patient, int>>(), It.IsAny<IUriService>(), It.IsAny<IMapper>(), mockLogger.Object);
            var result = await controller.Delete(4, It.IsAny<int>());

            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode == (int)System.Net.HttpStatusCode.BadRequest);
        }

    }
}
