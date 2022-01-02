using System.Collections.Generic;

namespace Fithub.Database.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CategoryType Type { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<Exercise> Exercises { get; set; }
    }
}
