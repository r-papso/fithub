using Fithub.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fithub.API.Interfaces
{
    public interface IExerciseService
    {
        public Task<IEnumerable<Exercise>> GetExercises(int userId, int categoryId);

        public Task<Exercise> AddExercise(Exercise exercise);

        public Task<Exercise> UpdateExercise(Exercise exercise);

        public Task<Exercise> DeleteExercise(Exercise exercise);
    }
}
