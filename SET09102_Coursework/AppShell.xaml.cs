namespace SET09102_Coursework;
using SET09102_Coursework.Views;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(AllUsersPage), typeof(AllUsersPage));
		Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
		Routing.RegisterRoute(nameof(AllSensorsPage), typeof(AllSensorsPage));
		Routing.RegisterRoute(nameof(SensorPage), typeof(SensorPage));
		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
		Routing.RegisterRoute(nameof(EditUserPage), typeof(EditUserPage));
	
	this.Navigated += async (s, e) =>
{
    if (e.Current?.Location.OriginalString == "//DashboardPage")
    {
        // Reset navigation stack ONLY if we came from another page
        await Shell.Current.GoToAsync("///DashboardPage");
    }
};

	
	}
}

