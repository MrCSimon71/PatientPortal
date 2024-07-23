using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using PDDS.PatientData.Api.Mappers;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.JWT;
using PDDS.PatientData.Core.Services;
using PDDS.PatientData.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task Post_ReturnsBadRequest_WhenPostingNullPatientDto()
        {
            var mockPatientService = new Mock<IPatientService<Patient, int>>();
            var mockLogger = new Mock<ILogger<PatientsController>>();
            var mockUriService = new Mock<IUriService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new PatientsController(mockPatientService.Object, mockUriService.Object, mockMapper.Object, mockLogger.Object);
            var result = await controller.Post(null);

            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode == (int)System.Net.HttpStatusCode.BadRequest);
        }

        //[Fact]
        //public async Task Post_ReturnsBadRequest_WhenPostingInvalidPatientDto()
        //{
        //    var mockPatientService = new Mock<IPatientService<Patient, int>>();
        //    var mockLogger = new Mock<ILogger<PatientsController>>();
        //    var mockUriService = new Mock<IUriService>();
        //    var mockMapper = new Mock<IMapper>();

        //    var controller = new PatientsController(mockPatientService.Object, mockUriService.Object, mockMapper.Object, mockLogger.Object);
        //    var result = await controller.Post(new DTOs.PatientDto()
        //    {
        //        FirstName = "Sue",
        //        LastName = "January",
        //        PrimaryPhone = null
        //    });

        //    Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode == (int)System.Net.HttpStatusCode.BadRequest);
        //}
    }
}
