using Fithub.API.Models;
using System.Threading.Tasks;

namespace Fithub.API.Interfaces
{
    public interface IUserService
    {
        public User GetUser(Credentials credentials);

        public Task<User> GetUserAsync(Credentials credentials);

        public bool AddUser(User user);

        public Task<bool> AddUserAsync(User user);

        public bool UpdateUser(User user);

        public Task<bool> UpdateUserAsync(User user);

        public bool DeleteUser(User user);

        public Task<bool> DeleteUserAsync(User user);
    }
}
