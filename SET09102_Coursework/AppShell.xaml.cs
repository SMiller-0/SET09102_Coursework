using CommunityToolkit.Mvvm.Input;

namespace SET09102_Coursework;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(Views.UserPage), typeof(Views.UserPage));

		Routing.RegisterRoute(nameof(Views.SensorPage), typeof(Views.SensorPage));

        Routing.RegisterRoute(nameof(Views.AdminBackUp), typeof(Views.AdminBackUp));

        Routing.RegisterRoute(nameof(Views.AnalyticalTestsEnviromental), typeof(Views.AnalyticalTestsEnviromental));

        Routing.RegisterRoute(nameof(Views.OpsAnomalyManager), typeof(Views.OpsAnomalyManager));
    }

    /*The idea for the next three functions is that, on user login, Preferences.Set("UserRole", VALUE_OF_USER_ROLE),
    allowing the information to be accessed across the application.*/
    //An even better way of doing this would be to save the entire User object in the preferences,
    //but that may not be feasible.

    [RelayCommand]
    public bool IsUserAdmin()
    {
        string currentUserRole = Preferences.Get("UserRole", "None");
        if (currentUserRole == "Administrator")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [RelayCommand]
    public bool IsUserEnviroIsUserEnviScientist()
    {
        string currentUserRole = Preferences.Get("UserRole", "None");
        if (currentUserRole == "EnvironmentalScientist")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [RelayCommand]
    public bool IsUserOps()
    {
        string currentUserRole = Preferences.Get("UserRole", "None");
        if (currentUserRole == "OperationsManager")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
