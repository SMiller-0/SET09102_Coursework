namespace SET09102_Coursework;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(Views.UserPage), typeof(Views.UserPage));
	}
}
