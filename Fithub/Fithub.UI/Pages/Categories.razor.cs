using Blazorise;
using Blazorise.Snackbar;
using Fithub.UI.Interfaces;
using Fithub.UI.Models;
using Fithub.UI.Services;
using Microsoft.AspNetCore.Components;
using System;

namespace Fithub.UI.Pages
{
    public partial class Categories
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
        protected ILocalStorage Storage { get; set; }

        #endregion

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Service.EntitiesChanged += OnEntitiesChanged;
            _ = Service.Request(null);
        }

        private async void AddCategory()
        {
            addModal.Hide();

            try
            {
                var user = await Storage.GetItem<User>("user");
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

        private void OnEntitiesChanged(object sender, EventArgs e)
        {
            _ = InvokeAsync(StateHasChanged);
        }
    }
}
