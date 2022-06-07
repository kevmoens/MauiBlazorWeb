using BlazorApp1;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RazorClassLibrary1.Data;
using MudBlazor.Services;
using Blazored.Modal;
using RazorClassLibrary1.Attachments;

namespace Company.WebApplication1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            builder.Services.AddMudServices();
            builder.Services.AddBlazoredModal();

            builder.Services.AddTransient<IAttachment, AttachmentUnsupported>();

            await builder.Build().RunAsync();
        }
    }
}