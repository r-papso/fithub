using Fithub.API.Interfaces;
using Fithub.API.Models;
using Fithub.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fithub.API.Services
{
    public class UserService : IUserService
    {
        private readonly FithubDbContext _dbContext;
        private readonly IHashService _hashService;
        private readonly IModelMapper _mapper;

        public UserService(FithubDbContext dbContext, IHashService hashService, IModelMapper mapper)
        {
            _dbContext = dbContext;
            _hashService = hashService;
            _mapper = mapper;
        }

        public bool AddUser(User user)
        {
            return AddUserAsync(user).Result;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            var existing = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username.Equals(user.Username));

            if (existing != null)
                return false;

            var dbUser = _mapper.Map(user);
            dbUser.Salt = _hashService.GenerateSalt();
            dbUser.Password = _hashService.CryptPassword(user.Password, dbUser.Salt);

            await _dbContext.Users.AddAsync(dbUser);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public bool UpdateUser(User user)
        {
            return UpdateUserAsync(user).Result;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var dbUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username.Equals(user.Username));

            if (dbUser == null)
                return false;

            var updated = _mapper.Map(user, dbUser);
            dbUser.Password = _hashService.CryptPassword(user.Password, dbUser.Salt);

            _dbContext.Users.Update(updated);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public bool DeleteUser(User user)
        {
            return DeleteUserAsync(user).Result;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            var dbUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username.Equals(user.Username));

            if (dbUser == null)
                return false;

            _dbContext.Users.Remove(dbUser);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public User GetUserById(int id)
        {
            return GetUserByIdAsync(id).Result;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var dbUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dbUser == null)
                return null;

            return _mapper.MapBack(dbUser);
        }

        public User GetUserByName(string name)
        {
            return GetUserByNameAsync(name).Result;
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            var dbUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username.Equals(name));

            if (dbUser == null)
                return null;

            return _mapper.MapBack(dbUser);
        }
    }
}
