using Fithub.API.Interfaces;

namespace Fithub.API.Services
{
    public class ModelMapper : IModelMapper
    {
        public Database.Models.User Map(Models.User user)
        {
            return new Database.Models.User()
            {
                Username = user.Username
            };
        }

        public Database.Models.User Map(Models.User user, Database.Models.User dbUser)
        {
            dbUser.Username = user.Username;
            return dbUser;
        }

        public Database.Models.Category Map(Models.Category category)
        {
            return new Database.Models.Category()
            {
                Name = category.Name,
                Type = category.Type,
                UserId = category.UserId
            };
        }

        public Database.Models.Category Map(Models.Category category, Database.Models.Category dbCategory)
        {
            dbCategory.Name = category.Name;
            dbCategory.Type = category.Type;
            dbCategory.UserId = category.UserId;

            return dbCategory;
        }

        public Database.Models.Exercise Map(Models.Exercise exercise)
        {
            return new Database.Models.Exercise()
            {
                Reps = exercise.Reps,
                Weight = exercise.Weight,
                Start = exercise.Start,
                End = exercise.End,
                Note = exercise.Note,
                CategoryId = exercise.CategoryId,
                UserId = exercise.UserId
            };
        }

        public Database.Models.Exercise Map(Models.Exercise exercise, Database.Models.Exercise dbExercise)
        {
            dbExercise.Reps = exercise.Reps;
            dbExercise.Weight = exercise.Weight;
            dbExercise.Start = exercise.Start;
            dbExercise.End = exercise.End;
            dbExercise.Note = exercise.Note;
            dbExercise.CategoryId = exercise.CategoryId;
            dbExercise.UserId = exercise.UserId;

            return dbExercise;
        }

        public Models.User MapBack(Database.Models.User user)
        {
            return new Models.User()
            {
                Id = user.Id,
                Username = user.Username
            };
        }

        public Models.Category MapBack(Database.Models.Category category)
        {
            return new Models.Category()
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type,
                UserId = category.UserId
            };
        }

        public Models.Exercise MapBack(Database.Models.Exercise exercise)
        {
            return new Models.Exercise()
            {
                Id = exercise.Id,
                Reps = exercise.Reps,
                Weight = exercise.Weight,
                Start = exercise.Start,
                End = exercise.End,
                Note = exercise.Note,
                CategoryId = exercise.CategoryId,
                UserId = exercise.UserId
            };
        }
    }
}
