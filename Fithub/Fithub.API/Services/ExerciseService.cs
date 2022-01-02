using Fithub.API.Interfaces;
using Fithub.API.Models;
using Fithub.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fithub.API.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly FithubDbContext _dbContext;
        private readonly IModelMapper _mapper;

        public ExerciseService(FithubDbContext dbContext, IModelMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool AddExercise(int categoryId, Exercise exercise)
        {
            return AddExerciseAsync(categoryId, exercise).Result;
        }

        public async Task<bool> AddExerciseAsync(int categoryId, Exercise exercise)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId);

            if (category == null)
                return false;

            category.Exercises.Add(_mapper.Map(exercise));
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public bool DeleteExercise(int categoryId, Exercise exercise)
        {
            return DeleteExerciseAsync(categoryId, exercise).Result;
        }

        public async Task<bool> DeleteExerciseAsync(int categoryId, Exercise exercise)
        {
            var dbExercise = await _dbContext.Exercises
                .FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.Id == exercise.Id);

            if (dbExercise == null)
                return false;

            _dbContext.Exercises.Remove(dbExercise);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public ICollection<Exercise> GetExercises(int categoryId)
        {
            return GetExercisesAsync(categoryId).Result;
        }

        public async Task<ICollection<Exercise>> GetExercisesAsync(int categoryId)
        {
            return await _dbContext.Exercises
                .Where(x => x.CategoryId == categoryId)
                .Select(x => _mapper.MapBack(x))
                .ToListAsync();
        }

        public bool UpdateExercise(int categoryId, Exercise exercise)
        {
            return UpdateExerciseAsync(categoryId, exercise).Result;
        }

        public async Task<bool> UpdateExerciseAsync(int categoryId, Exercise exercise)
        {
            var dbExercise = await _dbContext.Exercises
                .FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.Id == exercise.Id);

            if (dbExercise == null)
                return false;

            var updated = _mapper.Map(exercise, dbExercise);
            _dbContext.Exercises.Update(updated);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
