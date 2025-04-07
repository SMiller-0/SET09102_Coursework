using Microsoft.Extensions.Logging;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.ViewModels;
using SET09102_Coursework.Views;
namespace SET09102_Coursework;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		var a = Assembly.GetExecutingAssembly();
		using var stream = a.GetManifestResourceStream("SET09102_Coursework.appsettings.json");
    
		var config = new ConfigurationBuilder()
    		.AddJsonStream(stream)
    		.Build();
    
		builder.Configuration.AddConfiguration(config);

		var connectionString = builder.Configuration.GetConnectionString("LocalConnection");

		builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

		// ViewModels
		builder.Services.AddSingleton<AllUsersViewModel>();
		builder.Services.AddTransient<UserViewModel>();
		builder.Services.AddSingleton<AllSensorsViewModel>();
		builder.Services.AddTransient<SensorViewModel>();
		builder.Services.AddTransient<SensorSettingsViewModel>();

		// Views
		builder.Services.AddSingleton<AllUsersPage>();
		builder.Services.AddTransient<UserPage>();
		builder.Services.AddSingleton<AllSensorsPage>();
		builder.Services.AddTransient<SensorPage>();
		builder.Services.AddTransient<SensorSettingsPage>();

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
