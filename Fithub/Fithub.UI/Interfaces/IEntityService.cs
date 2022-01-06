using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fithub.UI.Interfaces
{
    public interface IEntityService<T>
    {
        public string Endpoint { get; }

        public IEnumerable<T> Get();

        public Task Request(object args);

        public Task Add(T entity);

        public Task Update(T entity);

        public Task Delete(T entity);

        public event EventHandler EntitiesChanged;
    }
}
