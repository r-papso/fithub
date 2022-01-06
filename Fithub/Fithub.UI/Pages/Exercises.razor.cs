using Blazorise;
using Blazorise.Snackbar;
using Fithub.UI.Interfaces;
using Fithub.UI.Models;
using Fithub.UI.Services;
using Microsoft.AspNetCore.Components;
using System;

namespace Fithub.UI.Pages
{
    public partial class Exercises : IDisposable
    {
        #region Models

        private Exercise selectedExercise = new();
        private Exercise newExercise = new();

        #endregion


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


        [Parameter]
        public int CategoryId { get; set; }

        [Inject]
        protected ExerciseService Service { get; set; }

        [Inject]
        protected IStateContainer Container { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Service.EntitiesChanged += OnEntitiesChanged;
            _ = Service.Request(CategoryId);
        }

        private void OnEntitiesChanged(object sender, EventArgs e)
        {
            _ = InvokeAsync(StateHasChanged);
        }

        private async void AddExercise()
        {
            addModal.Hide();

            try
            {
                newExercise.Start ??= DateTime.Now;
                newExercise.End ??= DateTime.Now;

                var user = Container.GetItem<User>("user");
                newExercise.UserId = user.Id;
                newExercise.CategoryId = CategoryId;

                await Service.Add(newExercise);

                successMessage = "Exercise successfully added!";
                successSnackbar.Show();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                errorSnackbar.Show();
            }
        }

        private async void UpdateExercise()
        {
            updateModal.Hide();

            try
            {
                selectedExercise.Start ??= DateTime.Now;
                selectedExercise.End ??= DateTime.Now;

                await Service.Update(selectedExercise);
                successMessage = "Exercise successfully updated!";
                successSnackbar.Show();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                errorSnackbar.Show();
            }
        }

        private async void DeleteExercise(Exercise exercise)
        {
            try
            {
                await Service.Delete(exercise);
                successMessage = "Exercise successfully deleted!";
                successSnackbar.Show();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                errorSnackbar.Show();
            }
        }

        public void Dispose()
        {
            Service.EntitiesChanged -= OnEntitiesChanged;
        }
    }
}
