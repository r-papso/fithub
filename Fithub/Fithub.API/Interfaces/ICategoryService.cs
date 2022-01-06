using Fithub.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fithub.API.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetCategories(int userId);

        public Task<Category> AddCategory(Category category);

        public Task<Category> UpdateCategory(Category category);

        public Task<Category> DeleteCategory(Category category);
    }
}
