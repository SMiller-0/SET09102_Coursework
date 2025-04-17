namespace SET09102_Coursework;

public partial class App : Application
{
    public static bool IsUserLoggedIn { get; set; } = false;

    public App()
    {
        InitializeComponent();
    
        MainPage = new AppShell();

        Shell.Current.GoToAsync("//LoginPage");

    }
    
}
