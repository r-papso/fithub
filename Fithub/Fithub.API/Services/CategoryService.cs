using Fithub.API.Interfaces;
using Fithub.API.Models;
using Fithub.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fithub.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly FithubDbContext _dbContext;
        private readonly IModelMapper _mapper;

        public CategoryService(FithubDbContext dbContext, IModelMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool AddCategory(int userId, Category category)
        {
            return AddCategoryAsync(userId, category).Result;
        }

        public async Task<bool> AddCategoryAsync(int userId, Category category)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return false;

            user.Categories.Add(_mapper.Map(category));
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public bool DeleteCategory(int userId, Category category)
        {
            return DeleteCategoryAsync(userId, category).Result;
        }

        public async Task<bool> DeleteCategoryAsync(int userId, Category category)
        {
            var dbCategory = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.UserId == userId && category.Id == x.Id);

            if (dbCategory == null)
                return false;

            _dbContext.Categories.Remove(dbCategory);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public ICollection<Category> GetCategories(int userId)
        {
            return GetCategoriesAsync(userId).Result;
        }

        public async Task<ICollection<Category>> GetCategoriesAsync(int userId)
        {
            return await _dbContext.Categories
                .Where(x => x.UserId == userId)
                .Select(x => _mapper.MapBack(x))
                .ToListAsync();
        }

        public bool UpdateCategory(int userId, Category category)
        {
            return UpdateCategoryAsync(userId, category).Result;
        }

        public async Task<bool> UpdateCategoryAsync(int userId, Category category)
        {
            var dbCategory = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.UserId == userId && category.Id == x.Id);

            if (dbCategory == null)
                return false;

            var updated = _mapper.Map(category, dbCategory);
            _dbContext.Categories.Update(updated);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
