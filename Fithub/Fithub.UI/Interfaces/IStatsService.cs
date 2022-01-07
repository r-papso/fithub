using Fithub.UI.Models.Stats;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fithub.UI.Interfaces
{
    public interface IStatsService
    {
        public IEnumerable<ExercisesByDay> StatsByDay();

        public IEnumerable<ExercisesByType> StatsByType();

        public Task Request();

        public event EventHandler StatsChanged;
    }
}
