using Fithub.UI.Interfaces;
using Fithub.UI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fithub.UI.Pages
{
    public partial class Register
    {
        private Registration registration = new();
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
                var creds = new Credentials() { Username = registration.Username, Password = registration.Password };
                await AuthService.Register(creds);
                NavigationManager.NavigateTo("/");
            }
            catch (Exception ex)
            {
                error = ex.Message;
                loading = false;
                StateHasChanged();
            }
        }

        private class Registration
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            [Compare(nameof(Password))]
            public string ConfirmPassword { get; set; }
        }
    }
}
