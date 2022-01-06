using System.Threading.Tasks;

namespace Fithub.UI.Interfaces
{
    public interface ILocalStorage
    {
        public Task<T> GetItem<T>(string key);

        public Task SetItem<T>(string key, T value);

        public Task RemoveItem(string key);
    }
}
