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
		Routing.RegisterRoute(nameof(SensorDashboardPage), typeof(SensorDashboardPage));
		Routing.RegisterRoute(nameof(UpdateFirmwarePage), typeof(UpdateFirmwarePage));
		Routing.RegisterRoute(nameof(SensorSettingsPage), typeof(SensorSettingsPage));
		Routing.RegisterRoute(nameof(UpdateSettingsPage), typeof(UpdateSettingsPage));
		Routing.RegisterRoute(nameof(CreateUserPage), typeof(CreateUserPage));
		Routing.RegisterRoute(nameof(AddSensorPage), typeof(AddSensorPage));
		Routing.RegisterRoute(nameof(EditSensorPage), typeof(EditSensorPage));
		Routing.RegisterRoute(nameof(SensorStatusPage), typeof(SensorStatusPage));
		Routing.RegisterRoute(nameof(CreateTicketPage), typeof(CreateTicketPage));
		//Routing.RegisterRoute(nameof(AllTicketsPage), typeof(AllTicketsPage));
		Routing.RegisterRoute(nameof(TicketDetailsPage), typeof(TicketDetailsPage));		Routing.RegisterRoute(nameof(SensorReportPage), typeof(SensorReportPage));
		Routing.RegisterRoute(nameof(TrendReportPage), typeof(TrendReportPage));
	}
}


