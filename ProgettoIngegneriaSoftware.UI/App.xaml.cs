using ProgettoIngegneriaSoftware.UI.ViewModels;
using ProgettoIngegneriaSoftware.UI.Views;

namespace ProgettoIngegneriaSoftware.UI
{
    public partial class App : Application
    {
        public App(AppShellViewModel appShellViewModel)
        {
            InitializeComponent();

            MainPage = new AppShell(appShellViewModel);
        }
    }
}