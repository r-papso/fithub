using Fithub.UI.Helpers;
using Fithub.UI.Interfaces;
using Fithub.UI.Models;
using System.Threading.Tasks;

namespace Fithub.UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpService _httpService;
        private readonly ILocalStorage _storage;
        private readonly IStateContainer _container;

        public AuthService(IHttpService httpService, ILocalStorage storage, IStateContainer container)
        {
            _httpService = httpService;
            _storage = storage;
            _container = container;
        }

        public async Task Initialize()
        {
            var user = await _storage.GetItem<User>("user");

            if (user != null)
                _container.SetItem("user", user);
        }

        public async Task Login(Credentials credentials)
        {
            var user = await _httpService.Post<User>(Endpoints.Auth, credentials);
            _container.SetItem("user", user);
            await _storage.SetItem("user", user);
        }

        public async Task Register(Credentials credentials)
        {
            var user = await _httpService.Post<User>(Endpoints.Register, credentials);
            _container.SetItem("user", user);
            await _storage.SetItem("user", user);
        }

        public async Task Logout()
        {
            _container.RemoveItem("user");
            await _storage.RemoveItem("user");
        }
    }
}
