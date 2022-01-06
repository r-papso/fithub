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

        public Exercise AddExercise(Exercise exercise)
        {
            return AddExerciseAsync(exercise).Result;
        }

        public async Task<Exercise> AddExerciseAsync(Exercise exercise)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == exercise.CategoryId);

            if (category == null)
                return null;

            var dbExercise = _mapper.Map(exercise);
            category.Exercises.Add(dbExercise);
            await _dbContext.SaveChangesAsync();

            return _mapper.MapBack(dbExercise);
        }

        public Exercise DeleteExercise(Exercise exercise)
        {
            return DeleteExerciseAsync(exercise).Result;
        }

        public async Task<Exercise> DeleteExerciseAsync(Exercise exercise)
        {
            var dbExercise = await _dbContext.Exercises
                .FirstOrDefaultAsync(x => x.CategoryId == exercise.CategoryId && x.Id == exercise.Id);

            if (dbExercise == null)
                return null;

            _dbContext.Exercises.Remove(dbExercise);
            await _dbContext.SaveChangesAsync();

            return _mapper.MapBack(dbExercise);
        }

        public ICollection<Exercise> GetExercises(int userId, int categoryId)
        {
            return GetExercisesAsync(userId, categoryId).Result;
        }

        public async Task<ICollection<Exercise>> GetExercisesAsync(int userId, int categoryId)
        {
            return await _dbContext.Exercises
                .Where(x => x.CategoryId == categoryId && x.UserId == userId)
                .Select(x => _mapper.MapBack(x))
                .ToListAsync();
        }

        public Exercise UpdateExercise(Exercise exercise)
        {
            return UpdateExerciseAsync(exercise).Result;
        }

        public async Task<Exercise> UpdateExerciseAsync(Exercise exercise)
        {
            var dbExercise = await _dbContext.Exercises
                .FirstOrDefaultAsync(x => x.CategoryId == exercise.CategoryId && x.Id == exercise.Id);

            if (dbExercise == null)
                return null;

            var updated = _mapper.Map(exercise, dbExercise);
            _dbContext.Exercises.Update(updated);
            await _dbContext.SaveChangesAsync();

            return _mapper.MapBack(updated);
        }
    }
}
