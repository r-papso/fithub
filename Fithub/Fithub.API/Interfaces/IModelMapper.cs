using Fithub.API.Models;

namespace Fithub.API.Interfaces
{
    public interface IModelMapper
    {
        public Database.Models.User Map(User user);

        public Database.Models.User Map(User user, Database.Models.User dbUser);

        public User MapBack(Database.Models.User user);
    }
}
