using Fithub.UI.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Fithub.UI.Services
{
    public class StateContainer : IStateContainer
    {
        private readonly IDictionary<string, object> _items;

        public StateContainer()
        {
            _items = new ConcurrentDictionary<string, object>();
        }

        public T GetItem<T>(string key)
        {
            if (!_items.TryGetValue(key, out object item))
                return default;

            if (!(item is T))
                return default;

            return (T)item;
        }

        public void RemoveItem(string key)
        {
            _items.Remove(key);
        }

        public void SetItem<T>(string key, T item)
        {
            _items[key] = item;
        }
    }
}
