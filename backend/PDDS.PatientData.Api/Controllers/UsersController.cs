using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Services;
using PDDS.PatientData.Api.Extensions;
using PDDS.PatientData.Api.DTOs;
using PDDS.PatientData.Core.Filters;
using PDDS.PatientData.Api.Helpers;
using PDDS.PatientData.Core.Attributes;

namespace PDDS.PatientData.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService<User, int> _userService;
        private readonly IUriService _uriService;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersController(IUserService<User, int> userService, IUriService uriService, IMapper mapper, ILogger<UsersController> logger)
        {
            _userService = userService;
            _uriService = uriService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: /users
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([ModelBinder(BinderType = typeof(UserFilterModelBinder))] SearchFilter searchFilter)
        {
            try
            {
                searchFilter.ApplyRules();

                var result = await _userService.GetAllAsync(searchFilter);

                var users = _mapper.Map<List<UserDto>>(result.Data.ToList());

                var pagedReponse = PaginationHelper.CreatePagedReponse<UserDto>(users, searchFilter, result.TotalRecords, _uriService, Request.Path.Value);

                return Ok(pagedReponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected in GET call: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // GET /users/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _userService.GetByIdAsync(id);

                var user = _mapper.Map<UserDto>(result);

                if (user != null)
                    return Ok(user);
                else
                    return NotFound("No data found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected in GET call: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // POST /users
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] UserDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var newUsers = await _userService.AddAsync(_mapper.Map<User>(user));

                return Created(string.Format("{0}/{1}", "/users", newUsers.UserID), 
                    _mapper.Map<UserDto>(newUsers));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while trying to save data: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // PUT /users/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] UserDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var response = await _userService.UpdateAsync(_mapper.Map<User>(user));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while trying to update data: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // DELETE /users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id, [FromQuery] int modifiedBy)
        {
            try
            {
                var response = await _userService.DeleteAsync(id, modifiedBy);

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
