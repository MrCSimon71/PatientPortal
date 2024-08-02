using Microsoft.AspNetCore.Mvc;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Services;
using PDDS.PatientData.Api.Extensions;
using PDDS.PatientData.Api.DTOs;
using AutoMapper;
using PDDS.PatientData.Api.Helpers;
using PDDS.PatientData.Core.Filters;
using PDDS.PatientData.Core.Attributes;

namespace PDDS.PatientData.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService<Patient, int> _patientService;
        private readonly IUriService _uriService;
        private readonly ILogger<PatientsController> _logger;
        private readonly IMapper _mapper;

        public PatientsController(IPatientService<Patient, int> patientService, IUriService uriService, IMapper mapper, ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _uriService = uriService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: /patients
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([ModelBinder(BinderType = typeof(PatientFilterModelBinder))] SearchFilter searchFilter)
        {
            try
            {
                searchFilter.ApplyRules();

                var result = await _patientService.GetAllAsync(searchFilter);

                var patients = _mapper.Map<List<PatientDto>>(result.Data.ToList());

                var pagedReponse = PaginationHelper.CreatePagedReponse<PatientDto>(patients, searchFilter, result.TotalRecords, _uriService, Request.Path.Value);

                return Ok(pagedReponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected in GET call: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // GET /patients/5
        /// <summary>
        /// Gets dental information for a specific patient.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns patient data for the requested resource</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Patients/5
        ///
        /// </remarks>
        /// <response code="200">Returns patient data for the requested resource</response>
        /// <response code="404">Returns 404 if resource not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _patientService.GetByIdAsync(id);

                var patient = _mapper.Map<PatientDto>(result);

                if (patient != null)
                    return Ok(patient);
                else
                    return NotFound("No data found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected in GET call: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // POST /patients
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] PatientDto patient)
        {
            try
            {
                if (patient == null)
                {
                    return BadRequest("Object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var newPatient = await _patientService.AddAsync(_mapper.Map<Patient>(patient));

                return Created(string.Format("{0}/{1}", "/patients", newPatient.PatientID), _mapper.Map<PatientDto>(newPatient));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while trying to save data: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // PUT /patients/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] PatientDto patient)
        {
            try
            {
                if (patient == null)
                {
                    return BadRequest("Object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (patient.PatientID != id)
                {
                    return BadRequest("Bad data");
                }

                var response = await _patientService.UpdateAsync(_mapper.Map<Patient>(patient));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while trying to update data: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // DELETE /patients/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id, [FromQuery] int modifiedBy)
        {
            try
            {
                if (id <= 0 || modifiedBy <= 0)
                {
                    return BadRequest("Bad data");
                }

                var response = await _patientService.DeleteAsync(id, modifiedBy);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while trying to delete data: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }

        }
    }
}
