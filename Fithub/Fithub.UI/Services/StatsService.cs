using Fithub.UI.Helpers;
using Fithub.UI.Interfaces;
using Fithub.UI.Models.Stats;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fithub.UI.Services
{
    public class StatsService : IStatsService
    {
        private readonly IHttpService _httpService;
        private readonly ICollection<ExercisesByDay> _statsByDay;
        private readonly ICollection<ExercisesByType> _statsByType;

        public StatsService(IHttpService httpService)
        {
            _httpService = httpService;
            _statsByDay = new List<ExercisesByDay>();
            _statsByType = new List<ExercisesByType>();
        }

        public event EventHandler StatsChanged;

        public async Task Request()
        {
            var byDay = await _httpService.Get<IEnumerable<ExercisesByDay>>(Endpoints.StatsByDay);

            _statsByDay.Clear();
            _statsByDay.AddRange(byDay);

            var byType = await _httpService.Get<IEnumerable<ExercisesByType>>(Endpoints.StatsByType);

            _statsByType.Clear();
            _statsByType.AddRange(byType);

            StatsChanged?.Invoke(this, EventArgs.Empty);
        }

        public IEnumerable<ExercisesByDay> StatsByDay()
        {
            return _statsByDay;
        }

        public IEnumerable<ExercisesByType> StatsByType()
        {
            return _statsByType;
        }
    }
}
