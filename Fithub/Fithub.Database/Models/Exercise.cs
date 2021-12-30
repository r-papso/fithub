using System;

namespace Fithub.Database.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        public int Reps { get; set; }

        public float Weight { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Note { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }
    }
}
