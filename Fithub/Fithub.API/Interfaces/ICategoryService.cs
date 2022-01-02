using Fithub.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fithub.API.Interfaces
{
    public interface ICategoryService
    {
        public ICollection<Category> GetCategories(int userId);

        public Task<ICollection<Category>> GetCategoriesAsync(int userId);

        public bool AddCategory(int userId, Category category);

        public Task<bool> AddCategoryAsync(int userId, Category category);

        public bool UpdateCategory(int userId, Category category);

        public Task<bool> UpdateCategoryAsync(int userId, Category category);

        public bool DeleteCategory(int userId, Category category);

        public Task<bool> DeleteCategoryAsync(int userId, Category category);
    }
}
