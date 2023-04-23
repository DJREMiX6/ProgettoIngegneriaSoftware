

using ProgettoIngegneriaSoftware.UI.Models;
using ProgettoIngegneriaSoftware.UI.ViewModels;

namespace ProgettoIngegneriaSoftware.UI.Views;

public partial class EventDetailView : ContentPage
{

    public EventDetailView(EventDetailViewModel eventDetailViewModel)
    {
        BindingContext = eventDetailViewModel;
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        ((EventDetailViewModel)BindingContext).NavigatedToCommand.Execute(null);
    }
}