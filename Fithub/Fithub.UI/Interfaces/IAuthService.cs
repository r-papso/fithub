using Fithub.UI.Models;
using System.Threading.Tasks;

namespace Fithub.UI.Interfaces
{
    public interface IAuthService
    {
        public Task Initialize();

        public Task Login(Credentials credentials);

        public Task Register(Credentials credentials);

        public Task Logout();
    }
}
