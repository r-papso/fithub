using Fithub.API.Models;
using System.Threading.Tasks;

namespace Fithub.API.Interfaces
{
    public interface IAuthService
    {
        public AuthData Authenticate(AuthData authData);

        public Task<AuthData> AuthenticateAsync(AuthData authData);

        public AuthData Authorize(AuthData authData);

        public Task<AuthData> AuthorizeAsync(AuthData authData);
    }
}
