using Fithub.UI.Helpers;
using Fithub.UI.Interfaces;
using Fithub.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fithub.UI.Services
{
    public class ExerciseService : EntityService<Exercise>
    {
        private ICollection<ExerciseDateGroup> _grouped;

        public ExerciseService(IHttpService httpService) : base(httpService)
        {
            _grouped = new List<ExerciseDateGroup>();
        }

        public override string Endpoint => Endpoints.Exercise;

        public override async Task Request(object args)
        {
            var categoryId = (int)args;
            var exercises = await HttpService.Get<IEnumerable<Exercise>>($"{Endpoint}/{categoryId}");

            Entities.Clear();
            Entities.AddRange(exercises);

            OnEntitiesChanged();
        }

        public ICollection<ExerciseDateGroup> GetExercisesGroupedByDate()
        {
            return _grouped;
        }

        protected override void OnEntitiesChanged()
        {
            _grouped = Entities
                .GroupBy(x => x.Start.Value.Date)
                .Select(g => new ExerciseDateGroup() { Date = g.Key, Exercises = g.ToList() })
                .OrderByDescending(x => x.Date)
                .ToList();

            base.OnEntitiesChanged();
        }

        protected override bool Compare(Exercise left, Exercise right)
        {
            return left.Id == right.Id;
        }
    }
}
