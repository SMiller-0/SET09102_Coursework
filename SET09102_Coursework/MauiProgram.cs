using Microsoft.Extensions.Logging;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.ViewModels;
using SET09102_Coursework.Views;
using SET09102_Coursework.Services;
using SET09102_Coursework.Validation;

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

		#if ANDROID
		var connectionString = builder.Configuration.GetConnectionString("AndroidConnection");
		#else
		var connectionString = builder.Configuration.GetConnectionString("LocalConnection");
		#endif

		builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

		builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

		// Services
		builder.Services.AddSingleton<ISensorService, SensorService>();
		builder.Services.AddSingleton<INavigationService, NavigationService>();
		builder.Services.AddSingleton<ISettingsValidator, SettingsValidator>();
		builder.Services.AddSingleton<ISensorValidator, SensorValidator>();
		builder.Services.AddSingleton<ITimerService, TimerService>();
		builder.Services.AddSingleton<ISensorRefreshService, SensorRefreshService>();
		builder.Services.AddSingleton<ISensorFilterService, SensorFilterService>();
		builder.Services.AddTransient<IMeasurementService, MeasurementService>();
		builder.Services.AddSingleton<IReportService, ReportService>();
		builder.Services.AddTransient<IMeasurementStrategy, AirMeasurementStrategy>();
		builder.Services.AddTransient<IMeasurementStrategy, WaterMeasurementStrategy>();
		builder.Services.AddTransient<IMeasurementStrategy, WeatherMeasurementStrategy>();

		// ViewModels
		builder.Services.AddSingleton<AllUsersViewModel>();
		builder.Services.AddTransient<UserViewModel>();
		builder.Services.AddSingleton<AllSensorsViewModel>();
		builder.Services.AddTransient<SensorViewModel>();
		builder.Services.AddTransient<SensorSettingsViewModel>();
		builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddTransient<DashboardViewModel>();
		builder.Services.AddSingleton<SensorDashboardViewModel>();
		builder.Services.AddTransient<UpdateFirmwareViewModel>();
		builder.Services.AddTransient<UpdateSettingsViewModel>();
		builder.Services.AddTransient<CreateUserViewModel>();
		builder.Services.AddTransient<AddSensorViewModel>();
		builder.Services.AddTransient<EditSensorViewModel>();
		builder.Services.AddTransient<SensorStatusViewModel>();
		builder.Services.AddTransient<SensorReportViewModel>();
		builder.Services.AddTransient<TrendReportViewModel>();

		// Views
		builder.Services.AddSingleton<AllUsersPage>();
		builder.Services.AddTransient<UserPage>();
		builder.Services.AddSingleton<AllSensorsPage>();
		builder.Services.AddTransient<SensorPage>();
		builder.Services.AddTransient<SensorSettingsPage>();
		builder.Services.AddSingleton<LoginPage>();
		builder.Services.AddSingleton<DashboardPage>();
		builder.Services.AddSingleton<SensorDashboardPage>();
		builder.Services.AddTransient<UpdateFirmwarePage>();
		builder.Services.AddTransient<UpdateSettingsPage>();
		builder.Services.AddTransient<CreateUserPage>();
		builder.Services.AddTransient<AddSensorPage>();
		builder.Services.AddTransient<EditSensorPage>();
		builder.Services.AddTransient<SensorStatusPage>();
		builder.Services.AddTransient<SensorReportPage>();
		builder.Services.AddTransient<TrendReportPage>();

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

		// Register route
		Routing.RegisterRoute(nameof(TrendReportPage), typeof(TrendReportPage));

		return builder.Build();
	}
}
