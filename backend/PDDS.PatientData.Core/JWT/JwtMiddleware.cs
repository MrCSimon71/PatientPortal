using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Helpers;
using PDDS.PatientData.Core.Services;
using IPDDSAuthenticationService = PDDS.PatientData.Core.Services.IAuthenticationService<PDDS.PatientData.Core.Entities.User, int>;

namespace PDDS.PatientData.Core.JWT
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IPDDSAuthenticationService authenticationService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, authenticationService, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IPDDSAuthenticationService authenticationService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,                    
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userID = Convert.ToInt32(jwtToken.Claims.First(x => x.Type == "UserID").Value);

                context.Items["User"] = authenticationService.GetByIdAsync(userID).Result;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
