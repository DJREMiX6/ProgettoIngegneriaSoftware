using ProgettoIngegneriaSoftware.UI.ViewModels;
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
        }
    }
}