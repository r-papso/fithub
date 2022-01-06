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

        public async Task<User> AddUser(User user)
        {
            var existing = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username.Equals(user.Username));

            if (existing != null)
                return null;

            var dbUser = _mapper.Map(user);
            dbUser.Salt = _hashService.GenerateSalt();
            dbUser.Password = _hashService.CryptPassword(user.Password, dbUser.Salt);

            var added = await _dbContext.Users.AddAsync(dbUser);
            await _dbContext.SaveChangesAsync();

            return _mapper.MapBack(added.Entity);
        }

        public async Task<User> UpdateUser(User user)
        {
            var dbUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username.Equals(user.Username));

            if (dbUser == null)
                return null;

            var updated = _mapper.Map(user, dbUser);
            dbUser.Password = _hashService.CryptPassword(user.Password, dbUser.Salt);

            _dbContext.Users.Update(updated);
            await _dbContext.SaveChangesAsync();

            return _mapper.MapBack(updated);
        }

        public async Task<User> DeleteUser(User user)
        {
            var dbUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username.Equals(user.Username));

            if (dbUser == null)
                return null;

            _dbContext.Users.Remove(dbUser);
            await _dbContext.SaveChangesAsync();

            return _mapper.MapBack(dbUser);
        }

        public async Task<User> GetUserById(int id)
        {
            var dbUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dbUser == null)
                return null;

            return _mapper.MapBack(dbUser);
        }

        public async Task<User> GetUserByName(string name)
        {
            var dbUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username.Equals(name));

            if (dbUser == null)
                return null;

            return _mapper.MapBack(dbUser);
        }
    }
}
