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
            var newGrouped = Entities
                .GroupBy(e => e.Start.Value.Date)
                .Select(g => new ExerciseDateGroup()
                {
                    Date = g.Key,
                    Exercises = g.OrderByDescending(e => e.Start).ToList(),
                    CategoryId = g.FirstOrDefault()?.CategoryId ?? 0
                })
                .OrderByDescending(g => g.Date)
                .ToList();

            if (_grouped.Count > 0 && newGrouped.Count > 0 && _grouped.First().CategoryId == newGrouped.First().CategoryId)
            {
                var join = newGrouped
                    .GroupJoin(
                        _grouped,
                        left => left.Date,
                        right => right.Date,
                        (left, right) => new { left, right = right.DefaultIfEmpty() })
                    .SelectMany(join => join.right.Select(right => new { join.left, right }))
                    .Select(
                        join => new ExerciseDateGroup()
                        {
                            Date = join.left.Date,
                            Exercises = join.left.Exercises.OrderByDescending(e => e.Start).ToList(),
                            CategoryId = join.left.CategoryId,
                            Visible = join.right?.Visible ?? false
                        })
                    .OrderByDescending(g => g.Date)
                    .ToList();

                _grouped = join;
            }
            else
            {
                _grouped = newGrouped;
            }

            base.OnEntitiesChanged();
        }

        protected override bool Compare(Exercise left, Exercise right)
        {
            return left.Id == right.Id;
        }
    }
}
