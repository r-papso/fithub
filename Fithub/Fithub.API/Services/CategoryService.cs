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

        public async Task<Category> AddCategory(Category category)
        {
            var dbCategory = _mapper.Map(category);
            var added = await _dbContext.Categories.AddAsync(dbCategory);
            await _dbContext.SaveChangesAsync();

            return _mapper.MapBack(added.Entity);
        }

        public async Task<Category> DeleteCategory(Category category)
        {
            var dbCategory = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.UserId == category.UserId && category.Id == x.Id);

            if (dbCategory == null)
                return null;

            _dbContext.Categories.Remove(dbCategory);
            await _dbContext.SaveChangesAsync();

            return _mapper.MapBack(dbCategory);
        }

        public async Task<IEnumerable<Category>> GetCategories(int userId)
        {
            return await _dbContext.Categories
                .Where(x => x.UserId == userId)
                .Select(x => _mapper.MapBack(x))
                .ToListAsync();
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var dbCategory = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.UserId == category.UserId && category.Id == x.Id);

            if (dbCategory == null)
                return null;

            var updated = _mapper.Map(category, dbCategory);
            _dbContext.Categories.Update(updated);
            await _dbContext.SaveChangesAsync();

            return _mapper.MapBack(updated);
        }
    }
}
