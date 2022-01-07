using Fithub.UI.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Fithub.UI.Pages
{
    public partial class Logout
    {
        [Inject]
        protected IAuthService AuthService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await AuthService.Logout();
            NavigationManager.NavigateTo("/login");
        }
    }
}
