using Fithub.UI.Helpers;
using Fithub.UI.Interfaces;
using Fithub.UI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fithub.UI.Services
{
    public class ExerciseService : EntityService<Exercise>
    {
        public ExerciseService(IHttpService httpService) : base(httpService)
        { }

        public override string Endpoint => Endpoints.Exercise;

        public override async Task Request(object args)
        {
            var categoryId = (int)args;
            var exercises = await HttpService.Get<IEnumerable<Exercise>>($"{Endpoint}/{categoryId}");

            Entities.Clear();
            foreach (var exercise in exercises)
            {
                Entities.Add(exercise);
            }

            OnEntitiesChanged();
        }

        protected override bool Compare(Exercise left, Exercise right)
        {
            return left.Id == right.Id;
        }
    }
}
