using System;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

public partial class DashboardViewModel: ObservableObject
{
    private readonly ICurrentUserService _currentUserService;

        public DashboardViewModel(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public string WelcomeMessage
        {
            get
            {
                var user = _currentUserService.LoggedInUser;
                if (user == null)
                {
                    return "Welcome!";
                }

                return $"Welcome, {user.FirstName}!";
            }
        }

}