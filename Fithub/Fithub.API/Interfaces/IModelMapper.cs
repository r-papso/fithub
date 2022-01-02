using Fithub.API.Models;

namespace Fithub.API.Interfaces
{
    public interface IModelMapper
    {
        public Database.Models.User Map(User user);

        public Database.Models.User Map(User user, Database.Models.User dbUser);

        public Database.Models.Category Map(Category category);

        public Database.Models.Category Map(Category category, Database.Models.Category dbCategory);

        public Database.Models.Exercise Map(Exercise exercise);

        public Database.Models.Exercise Map(Exercise exercise, Database.Models.Exercise dbExercise);

        public User MapBack(Database.Models.User user);

        public Category MapBack(Database.Models.Category category);

        public Exercise MapBack(Database.Models.Exercise exercise);
    }
}
