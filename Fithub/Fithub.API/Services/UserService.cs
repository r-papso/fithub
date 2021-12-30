using Fithub.API.Interfaces;
using Fithub.API.Models;
using Fithub.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Fithub.API.Services
{
    public class UserService : IUserService
    {
        private const int SaltLength = 32;

        private readonly FithubDbContext _dbContext;
        private readonly IHashService _hashService;
        private readonly IModelMapper _mapper;

        public UserService(FithubDbContext dbContext, IHashService hashService, IModelMapper mapper)
        {
            _dbContext = dbContext;
            _hashService = hashService;
            _mapper = mapper;
        }

        public User GetUser(Credentials credentials)
        {
            throw new NotImplementedException($"Not implemented, use {nameof(GetUserAsync)} instead");
        }

        public async Task<User> GetUserAsync(Credentials credentials)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username.Equals(credentials.Username));

            if (user == null)
                return null;

            return _mapper.MapBack(user);
        }

        public bool AddUser(User user)
        {
            throw new NotImplementedException($"Not implemented, use {nameof(AddUserAsync)} instead");
        }

        public async Task<bool> AddUserAsync(User user)
        {
            if (_dbContext.Users.Any(x => x.Username.Equals(user.Username)))
                return false;

            var newUser = new Database.Models.User();
            newUser.Salt = _hashService.GenerateSalt();
            newUser.Password = _hashService.CryptPassword(user.Password, newUser.Salt);

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException($"Not implemented, use {nameof(UpdateUserAsync)} instead");
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var dbUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username.Equals(user.Username));

            if (dbUser == null)
                return false;

            var updatedUser = _mapper.Map(user, dbUser);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException($"Not implemented, use {nameof(DeleteUserAsync)} instead");
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            var dbUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username.Equals(user.Username));

            if (dbUser == null)
                return false;

            _dbContext.Users.Remove(dbUser);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
