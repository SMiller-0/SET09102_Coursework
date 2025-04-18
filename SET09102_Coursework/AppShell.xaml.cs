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
		Routing.RegisterRoute(nameof(CreateUserPage), typeof(CreateUserPage));
	
	}
}

