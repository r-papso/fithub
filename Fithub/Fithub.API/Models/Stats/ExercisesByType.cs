using Fithub.Database.Models;

namespace Fithub.API.Models.Stats
{
    public class ExercisesByType
    {
        public CategoryType Type { get; set; }

        public int Count { get; set; }
    }
}
