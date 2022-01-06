using Fithub.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fithub.API.Interfaces
{
    public interface ICategoryService
    {
        public ICollection<Category> GetCategories(int userId);

        public Task<ICollection<Category>> GetCategoriesAsync(int userId);

        public Category AddCategory(Category category);

        public Task<Category> AddCategoryAsync(Category category);

        public Category UpdateCategory(Category category);

        public Task<Category> UpdateCategoryAsync(Category category);

        public Category DeleteCategory(Category category);

        public Task<Category> DeleteCategoryAsync(Category category);
    }
}
