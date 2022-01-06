using Fithub.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fithub.UI.Services
{
    public abstract class EntityService<T> : IEntityService<T>
    {
        protected IHttpService HttpService { get; }
        protected ICollection<T> Entities { get; }

        public EntityService(IHttpService httpService)
        {
            HttpService = httpService;
            Entities = new List<T>();
        }

        public abstract string Endpoint { get; }

        public event EventHandler EntitiesChanged;

        public async Task Add(T entity)
        {
            var added = await HttpService.Post<T>(Endpoint, entity);
            Entities.Add(added);
            OnEntitiesChanged();
        }

        public async Task Delete(T entity)
        {
            _ = await HttpService.Delete<T>(Endpoint, entity);
            Entities.Remove(entity);
            OnEntitiesChanged();
        }

        public IEnumerable<T> Get()
        {
            return Entities;
        }

        public abstract Task Request(object args);

        public async Task Update(T entity)
        {
            var updated = await HttpService.Put<T>(Endpoint, entity);
            var old = Entities.First(x => Compare(x, entity));
            Entities.Remove(old);
            Entities.Add(updated);
            OnEntitiesChanged();
        }

        protected abstract bool Compare(T left, T right);

        protected void OnEntitiesChanged()
        {
            EntitiesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
