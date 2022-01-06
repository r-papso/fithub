using Fithub.UI.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fithub.UI.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public CategoryType Type { get; set; }

        public int UserId { get; set; }

        public Category()
        { }

        public Category(Category other)
        {
            Id = other.Id;
            Name = other.Name;
            Type = other.Type;
            UserId = other.UserId;
        }
    }
}
