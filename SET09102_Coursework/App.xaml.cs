﻿namespace SET09102_Coursework;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    
        Routing.RegisterRoute(nameof(Views.NotePage), typeof(Views.NotePage));

        MainPage = new AppShell();
    }
}
