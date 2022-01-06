using Fithub.UI.Interfaces;
using Fithub.UI.Models;
using Microsoft.AspNetCore.Components;
using System;

namespace Fithub.UI.Pages
{
    public partial class Login
    {
        private Credentials credentials = new();
        private bool loading;
        private string error;

        [Inject]
        protected IAuthService AuthService { get; set; }

        [Inject]
        protected IStateContainer Container { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Container.GetItem<User>("user") != null)
                NavigationManager.NavigateTo("/");
        }

        private async void HandleValidSubmit()
        {
            loading = true;

            try
            {
                await AuthService.Login(credentials);
                NavigationManager.NavigateTo("/");
            }
            catch (Exception ex)
            {
                error = ex.Message;
                loading = false;
                StateHasChanged();
            }
        }
    }
}
