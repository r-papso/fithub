using Fithub.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fithub.API.Interfaces
{
    public interface IExerciseService
    {
        public ICollection<Exercise> GetExercises(int categoryId);

        public Task<ICollection<Exercise>> GetExercisesAsync(int categoryId);

        public bool AddExercise(int categoryId, Exercise exercise);

        public Task<bool> AddExerciseAsync(int categoryId, Exercise exercise);

        public bool UpdateExercise(int categoryId, Exercise exercise);

        public Task<bool> UpdateExerciseAsync(int categoryId, Exercise exercise);

        public bool DeleteExercise(int categoryId, Exercise exercise);

        public Task<bool> DeleteExerciseAsync(int categoryId, Exercise exercise);
    }
}
