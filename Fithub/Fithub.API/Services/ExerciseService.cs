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

        public async Task<Exercise> AddExercise(Exercise exercise)
        {
            var dbExercise = _mapper.Map(exercise);
            var added = _dbContext.Exercises.Add(dbExercise);
            await _dbContext.SaveChangesAsync();

            return _mapper.MapBack(added.Entity);
        }

        public async Task<Exercise> DeleteExercise(Exercise exercise)
        {
            var dbExercise = await _dbContext.Exercises
                .FirstOrDefaultAsync(x => x.CategoryId == exercise.CategoryId && x.Id == exercise.Id);

            if (dbExercise == null)
                return null;

            _dbContext.Exercises.Remove(dbExercise);
            await _dbContext.SaveChangesAsync();

            return _mapper.MapBack(dbExercise);
        }

        public async Task<IEnumerable<Exercise>> GetExercises(int userId, int categoryId)
        {
            return await _dbContext.Exercises
                .Where(x => x.CategoryId == categoryId && x.UserId == userId)
                .Select(x => _mapper.MapBack(x))
                .ToListAsync();
        }

        public async Task<Exercise> UpdateExercise(Exercise exercise)
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
