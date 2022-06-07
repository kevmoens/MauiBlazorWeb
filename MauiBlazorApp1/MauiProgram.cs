using Microsoft.AspNetCore.Components.WebView.Maui;
using RazorClassLibrary1.Data;
using MudBlazor.Services;
using Blazored.Modal;
using MauiBlazorApp1.Attachments;
using RazorClassLibrary1.Attachments;

namespace MauiBlazorApp1;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        builder.Services.AddMudServices();
        builder.Services.AddBlazoredModal();

        builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();


#if WINDOWS
		builder.Services.AddTransient<IAttachment,AttachmentWindows>();
#else
		builder.Services.AddTransient<IAttachment,AttachmentUnsupported>();
#endif

		return builder.Build();
	}
}
