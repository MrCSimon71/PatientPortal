using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Helpers;
using PDDS.PatientData.Core.Repositories;
using PDDS.PatientData.Core.Services;
using PDDS.PatientData.Core.Filters;
using PDDS.PatientData.Core.Entities;
using System.Net;
using PDDS.PatientData.Core.Middleware.JWT;

namespace PDDS.PatientData.Services
{
    public class AuthenticationService : IAuthenticationService<User, int>
    {
        private readonly IUserRepository<User, int> _userRepository;
        private readonly AppSettings _appSettings;

        public AuthenticationService(IUserRepository<User, int> userRepository, IOptions<AppSettings> appSettings)
        {
            this._userRepository = userRepository;
            this._appSettings = appSettings.Value;
        }

        public Task<JWToken> Authenticate(string username, string password)
        {
            var user = _userRepository.GetByUsernameAsync(username).Result;

            if (user == null || !PasswordHelper.VerifyPassword(password, user.Password))
            {
                return Task.FromResult<JWToken>(null);
            }

            var token = GenerateJWToken(user);

            return Task.FromResult(new JWToken()
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            });
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<User> Data, int TotalRecords)> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<User> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id, int modifiedBy)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        private string GenerateJWToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim("UserID", user.UserID.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
