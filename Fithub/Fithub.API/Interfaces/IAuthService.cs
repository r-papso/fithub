using Fithub.API.Models;
using System.Threading.Tasks;

namespace Fithub.API.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthData> Authenticate(AuthData authData);

        public Task<AuthData> Authorize(AuthData authData);
    }
}
