using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PDDS.PatientData.Core.Services;
using PDDS.PatientData.Api.Extensions;
using PDDS.PatientData.Api.DTOs;
using PDDS.PatientData.Core.Entities;

namespace PDDS.PatientData.Api.Controllers
{
    [Route("Auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService<User, int> _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService<User, int> authService, IMapper mapper, ILogger<AuthenticationController> logger)
        {
            _authService = authService;
            _logger = logger;
            _mapper = mapper;
        }

        // POST /login
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginData)
        {
            try
            {
                if (loginData == null)
                    return BadRequest("Invalid request data");

                if (string.IsNullOrEmpty(loginData.Username) || string.IsNullOrEmpty(loginData.Password))
                    return BadRequest("Invalid or missing login username / password");

                var jwToken = await _authService.Authenticate(loginData.Username, loginData.Password);

                if (jwToken != null)
                    return Ok(jwToken);
                else
                    return Unauthorized("");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while trying to save data: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LogOut([FromBody] LogOutDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Invalid request data");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while trying to save data: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }
    }
}