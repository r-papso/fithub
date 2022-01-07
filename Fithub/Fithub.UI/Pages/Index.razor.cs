using Blazorise.Charts;
using Fithub.UI.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fithub.UI.Pages
{
    public partial class Index
    {
        #region Charts

        private LineChart<int> dayChart;
        private Chart<int> typeChart;

        private LineChartOptions dayChartOptions = new() { AspectRatio = 1.5 };
        private ChartOptions typeChartOptions = new() { AspectRatio = 1.5 };

        private List<string> backgroundColors = new()
        {
            ChartColor.FromRgba(255, 99, 132, 0.2f),
            ChartColor.FromRgba(54, 162, 235, 0.2f),
            ChartColor.FromRgba(255, 206, 86, 0.2f),
            ChartColor.FromRgba(75, 192, 192, 0.2f),
            ChartColor.FromRgba(153, 102, 255, 0.2f),
            ChartColor.FromRgba(255, 159, 64, 0.2f)
        };

        private List<string> borderColors = new()
        {
            ChartColor.FromRgba(255, 99, 132, 1f),
            ChartColor.FromRgba(54, 162, 235, 1f),
            ChartColor.FromRgba(255, 206, 86, 1f),
            ChartColor.FromRgba(75, 192, 192, 1f),
            ChartColor.FromRgba(153, 102, 255, 1f),
            ChartColor.FromRgba(255, 159, 64, 1f)
        };

        #endregion


        [Inject]
        protected IStatsService Service { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Service.StatsChanged += OnStatsChanged;
            await Service.Request();
        }

        private async void OnStatsChanged(object sender, System.EventArgs e)
        {
            await dayChart.Clear();
            await dayChart.AddLabelsDatasetsAndUpdate(GetDayChartLabels(), GetDayChartDataset());

            await typeChart.Clear();
            await typeChart.AddLabelsDatasetsAndUpdate(GetTypeChartLabels(), GetTypeChartDataset());

            _ = InvokeAsync(StateHasChanged);
        }

        private IReadOnlyCollection<string> GetDayChartLabels()
        {
            return Service.StatsByDay().Select(x => x.Date.ToString("dd.MM.yyyy")).ToList().AsReadOnly();
        }

        private LineChartDataset<int> GetDayChartDataset()
        {
            return new LineChartDataset<int>()
            {
                Label = "# of Exercises",
                Data = Service.StatsByDay().Select(x => x.Count).ToList(),
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                Fill = true,
                PointRadius = 2,
                BorderDash = new List<int>()
            };
        }

        private IReadOnlyCollection<string> GetTypeChartLabels()
        {
            return Service.StatsByType().Select(x => x.Type.ToString()).ToList().AsReadOnly();
        }

        private DoughnutChartDataset<int> GetTypeChartDataset()
        {
            return new DoughnutChartDataset<int>()
            {
                Data = Service.StatsByType().Select(x => x.Count).ToList(),
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                BorderWidth = 1
            };
        }
    }
}
