using System.Threading.Tasks;

namespace Fithub.UI.Interfaces
{
    public interface IHttpService
    {
        public Task<T> Get<T>(string uri);

        public Task<T> Post<T>(string uri, object value);

        public Task<T> Put<T>(string uri, object value);

        public Task<T> Delete<T>(string uri, object value);
    }
}
