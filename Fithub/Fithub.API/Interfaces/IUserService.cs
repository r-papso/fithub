using Fithub.API.Models;
using System.Threading.Tasks;

namespace Fithub.API.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserById(int id);

        public Task<User> GetUserByName(string name);

        public Task<User> AddUser(User user);

        public Task<User> UpdateUser(User user);

        public Task<User> DeleteUser(User user);
    }
}
