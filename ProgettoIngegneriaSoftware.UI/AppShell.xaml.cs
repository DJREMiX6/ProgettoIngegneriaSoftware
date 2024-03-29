﻿using ProgettoIngegneriaSoftware.UI.ViewModels;
using ProgettoIngegneriaSoftware.UI.Views;

namespace ProgettoIngegneriaSoftware.UI
{
    public partial class AppShell : Shell
    {

        public AppShell(AppShellViewModel appShellViewModel)
        {
            InitializeComponent();

            BindingContext = appShellViewModel;
            Routing.RegisterRoute(nameof(QrCodeScanPage), typeof(QrCodeScanPage));
            Routing.RegisterRoute(nameof(QrCodeDetailView), typeof(QrCodeDetailView));
            Routing.RegisterRoute(nameof(EventDetailView), typeof(EventDetailView));
            Routing.RegisterRoute(nameof(EventsPage), typeof(EventsPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(BookTicketsDetailView), typeof(BookTicketsDetailView));
            Routing.RegisterRoute(nameof(BookedTicketsDetailView), typeof(BookedTicketsDetailView));
        }
    }
}