using System;
using System.Collections.Generic;

namespace Fithub.UI.Models
{
    public class ExerciseDateGroup
    {
        public DateTime Date { get; set; }

        public IEnumerable<Exercise> Exercises { get; set; }

        public bool Visible { get; set; }

        public int CategoryId { get; set; }

        public ExerciseDateGroup()
        { }

        public ExerciseDateGroup(DateTime date, IEnumerable<Exercise> exercises)
        {
            Date = date;
            Exercises = exercises;
        }
    }
}
