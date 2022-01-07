using Blazorise;
using Blazorise.Snackbar;
using Fithub.UI.Interfaces;
using Fithub.UI.Models;
using Fithub.UI.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Fithub.UI.Pages
{
    public partial class Categories : IDisposable
    {
        #region Modal

        private Modal updateModal;
        private Modal addModal;

        #endregion


        #region Snackbar

        private Snackbar successSnackbar;
        private Snackbar errorSnackbar;

        private string successMessage;
        private string errorMessage;

        #endregion


        #region Models

        private Category selectedCategory = new();
        private Category newCategory = new();

        #endregion


        #region Services

        [Inject]
        protected CategoryService Service { get; set; }

        [Inject]
        protected IStateContainer Container { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        #endregion

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Service.EntitiesChanged += OnEntitiesChanged;
            await Service.Request(null);
        }

        private async void AddCategory()
        {
            addModal.Hide();

            try
            {
                var user = Container.GetItem<User>("user");
                newCategory.UserId = user?.Id ?? -1;
                await Service.Add(newCategory);
                successMessage = "Category successfully added!";
                successSnackbar.Show();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                errorSnackbar.Show();
            }

            newCategory = new();
        }

        private async void UpdateCategory()
        {
            updateModal.Hide();

            try
            {
                await Service.Update(selectedCategory);
                successMessage = "Category successfully updated!";
                successSnackbar.Show();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                errorSnackbar.Show();
            }
        }

        private async void DeleteCategory(Category category)
        {
            try
            {
                await Service.Delete(category);
                successMessage = "Category successfully deleted!";
                successSnackbar.Show();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                errorSnackbar.Show();
            }
        }

        private void NavigateToExercises(Category category)
        {
            NavigationManager.NavigateTo($"/exercises/{category.Id}");
        }

        private void OnEntitiesChanged(object sender, EventArgs e)
        {
            Service.Sort(x => x.Name);
            _ = InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            Service.EntitiesChanged -= OnEntitiesChanged;
        }
    }
}
