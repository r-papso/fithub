using Fithub.API.Interfaces;
using Fithub.API.Models.Stats;
using Fithub.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fithub.API.Services
{
    public class StatsService : IStatsService
    {
        private readonly FithubDbContext _dbContext;

        public StatsService(FithubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ExercisesByDay>> GetExerciseCountByDay(int userId)
        {
            var result = await _dbContext.Exercises
                .Where(e => e.UserId == userId)
                .GroupBy(e => e.Start.Date)
                .Select(g => new ExercisesByDay() { Date = g.Key, Count = g.Count() })
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<ExercisesByType>> GetExerciseCountByType(int userId)
        {
            var result = await _dbContext.Categories
                .Where(c => c.UserId == userId)
                .SelectMany(c => c.Exercises, (c, e) => new { Category = c, Exercise = e })
                .GroupBy(x => x.Category.Type)
                .Select(g => new ExercisesByType() { Type = g.Key, Count = g.Count() })
                .ToListAsync();

            return result;
        }
    }
}
