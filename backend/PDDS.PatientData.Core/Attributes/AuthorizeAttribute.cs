using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PDDS.PatientData.Core.Entities;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace PDDS.PatientData.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!HasAllowAnonymous(context))
            {
                var user = (User)context.HttpContext.Items["User"];

                if (user == null) // not logged in
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }

        private bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Filters.Any(item => item is IAllowAnonymousFilter);
        }
    }
}
