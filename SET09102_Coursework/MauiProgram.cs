using Microsoft.Extensions.Logging;
using System.Reflection;
using Microsoft.Extensions.Configuration;

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
