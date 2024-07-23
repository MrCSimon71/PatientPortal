using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace PDDS.PatientData.Api.Extensions
{
    public static class ControllerBaseExtension
    {
        public static ObjectResult InternalServerError(this ControllerBase controller, int errorNumber, string message)
        {
            var statusMessage = new
            {
                errorNo = errorNumber,
                message = message
            };

            return controller.StatusCode(StatusCodes.Status500InternalServerError, statusMessage);
        }

        public static Response<T> CreateResponse<T>(this ControllerBase controller, T data)
        {
            var response = new Response<T>(data);
            return response;
        }
    }
}
