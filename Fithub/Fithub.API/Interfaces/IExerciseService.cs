using Fithub.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fithub.API.Interfaces
{
    public interface IExerciseService
    {
        public ICollection<Exercise> GetExercises(int userId, int categoryId);

        public Task<ICollection<Exercise>> GetExercisesAsync(int userId, int categoryId);

        public Exercise AddExercise(Exercise exercise);

        public Task<Exercise> AddExerciseAsync(Exercise exercise);

        public Exercise UpdateExercise(Exercise exercise);

        public Task<Exercise> UpdateExerciseAsync(Exercise exercise);

        public Exercise DeleteExercise(Exercise exercise);

        public Task<Exercise> DeleteExerciseAsync(Exercise exercise);
    }
}
