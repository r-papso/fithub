using Fithub.Database.Models;

namespace Fithub.API.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CategoryType Type { get; set; }

        public int UserId { get; set; }
    }
}
