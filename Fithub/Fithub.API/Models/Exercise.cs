using System;

namespace Fithub.API.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        public int Reps { get; set; }

        public float Weight { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Note { get; set; }
    }
}
