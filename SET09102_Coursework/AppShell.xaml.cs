namespace SET09102_Coursework;
using SET09102_Coursework.Views;


public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(AllUsersPage), typeof(AllUsersPage));
		Routing.RegisterRoute(nameof(UserPage), typeof(Views.UserPage));
		Routing.RegisterRoute(nameof(SensorPage), typeof(Views.SensorPage));
		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

	}

}
