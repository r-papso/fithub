using Fithub.API.Interfaces;
using Fithub.API.Models;
using Fithub.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fithub.API.Services
{
    public class JwtAuthService : IAuthService
    {
        private const int DaysOfTokenValidity = 7;

        private readonly FithubDbContext _dbContext;
        private readonly IHashService _hashService;

        public JwtAuthService(IHashService hashService, FithubDbContext dbContext)
        {
            _hashService = hashService;
            _dbContext = dbContext;
        }

        public AuthData Authenticate(AuthData authData)
        {
            return AuthenticateAsync(authData).Result;
        }

        public async Task<AuthData> AuthenticateAsync(AuthData authData)
        {
            var creds = authData.Authentication as Credentials;
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username.Equals(creds.Username));

            if (user == null)
                return null;

            if (!_hashService.CryptPassword(creds.Password, user.Salt).Equals(user.Password))
                return null;

            var jwt = GenerateJwtToken(user);
            return new AuthData() { Authentication = jwt };
        }

        public AuthData Authorize(AuthData authData)
        {
            try
            {
                var token = authData.Authorization as string;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("SECRET"); // TODO change SECRET

                var validationParams = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return new AuthData() { Authorization = userId };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<AuthData> AuthorizeAsync(AuthData authData)
        {
            return Task.FromResult(Authorize(authData));
        }

        private string GenerateJwtToken(Database.Models.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SECRET"); // TODO change SECRET

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(DaysOfTokenValidity),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
