using Fithub.API.Models.Stats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fithub.API.Interfaces
{
    public interface IStatsService
    {
        public Task<IEnumerable<ExercisesByDay>> GetExerciseCountByDay(int userId);

        public Task<IEnumerable<ExercisesByType>> GetExerciseCountByType(int userId);
    }
}
