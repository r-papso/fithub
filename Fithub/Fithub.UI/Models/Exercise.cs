using System;

namespace Fithub.UI.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        public int Reps { get; set; }

        public float Weight { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public string Note { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }

        public Exercise()
        { }

        public Exercise(Exercise other)
        {
            Id = other.Id;
            Reps = other.Reps;
            Weight = other.Weight;
            Start = other.Start;
            End = other.End;
            Note = other.Note;
            CategoryId = other.CategoryId;
            UserId = other.UserId;
        }
    }
}
