using Fithub.API.Helpers;
using Fithub.API.Interfaces;
using Fithub.API.Models;
using Fithub.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        private readonly AppSettings _appSettings;
        private readonly IModelMapper _mapper;

        public JwtAuthService(IHashService hashService, FithubDbContext dbContext, IOptions<AppSettings> options, IModelMapper mapper)
        {
            _hashService = hashService;
            _dbContext = dbContext;
            _appSettings = options.Value;
            _mapper = mapper;
        }

        public async Task<AuthData> Authenticate(AuthData authData)
        {
            var creds = authData.Authentication as Credentials;
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username.Equals(creds.Username));

            if (user == null)
                return null;

            if (!_hashService.CryptPassword(creds.Password, user.Salt).Equals(user.Password))
                return null;

            var jwt = GenerateJwtToken(user);
            var apiUser = _mapper.MapBack(user);
            apiUser.Token = jwt;

            return new AuthData() { Authentication = apiUser };
        }

        public async Task<AuthData> Authorize(AuthData authData)
        {
            try
            {
                var token = authData.Authorization as string;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

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

                if (!await _dbContext.Users.AnyAsync(x => x.Id == userId))
                    return null;

                return new AuthData() { Authorization = userId };
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GenerateJwtToken(Database.Models.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

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
