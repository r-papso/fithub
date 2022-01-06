namespace Fithub.UI.Interfaces
{
    public interface IStateContainer
    {
        public T GetItem<T>(string key);

        public void SetItem<T>(string key, T item);

        public void RemoveItem(string key);
    }
}
