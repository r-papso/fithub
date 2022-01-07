using Fithub.UI.Interfaces;
using Fithub.UI.Models;
using Microsoft.AspNetCore.Components;

namespace Fithub.UI.Shared
{
    public partial class MainLayout
    {
        [Inject]
        protected IStateContainer Container { get; set; }

        private User User => Container.GetItem<User>("user");

        private string Username => User?.Username ?? string.Empty;
    }
}
