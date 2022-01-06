using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Fithub.UI.Interfaces;
using Fithub.UI.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fithub.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services
              .AddBlazorise(options => { options.ChangeTextOnKeyPress = true; })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["apiUrl"]) });
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddScoped<ILocalStorage, LocalStorage>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<ExerciseService>();

            builder.Services.AddSingleton<IStateContainer, StateContainer>();

            var host = builder.Build();

            var authService = host.Services.GetRequiredService<IAuthService>();
            await authService.Initialize();

            await host.RunAsync();
        }
    }
}
