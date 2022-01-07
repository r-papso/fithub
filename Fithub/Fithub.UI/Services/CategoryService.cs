using Fithub.UI.Helpers;
using Fithub.UI.Interfaces;
using Fithub.UI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fithub.UI.Services
{
    public class CategoryService : EntityService<Category>
    {
        public CategoryService(IHttpService httpService) : base(httpService)
        { }

        public override string Endpoint => Endpoints.Category;

        public override async Task Request(object args)
        {
            var categories = await HttpService.Get<IEnumerable<Category>>(Endpoint);

            Entities.Clear();
            Entities.AddRange(categories);

            OnEntitiesChanged();
        }

        protected override bool Compare(Category left, Category right)
        {
            return left.Id == right.Id;
        }
    }
}
