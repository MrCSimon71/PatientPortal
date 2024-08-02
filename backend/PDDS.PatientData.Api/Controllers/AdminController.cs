using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PDDS.PatientData.Api.DTOs;
using PDDS.PatientData.Api.Extensions;
using PDDS.PatientData.Api.Helpers;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Filters;
using PDDS.PatientData.Core.Helpers;
using PDDS.PatientData.Core.Services;
using PDDS.PatientData.Services;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PDDS.PatientData.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        // POST: admin/database/reset
        [SwaggerIgnore]
        [HttpPost("database/reset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetDatabase()
        {
            try
            {
                DatabaseHelper.InitializeDatabase(true);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected in GET call: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            throw new ArgumentNullException("Value can't be null or empty");
            //try
            //{
            //    throw new ArgumentNullException("Value can't be null or empty");
            //}
            //catch (Exception ex)
            //{
            //}

            //return Ok();
        }
    }
}
