using ProgettoIngegneriaSoftware.UI.ViewModels;

namespace ProgettoIngegneriaSoftware.UI.Views;

public partial class EventsPage : ContentPage
{

    #region FIELDS

    private readonly EventsViewModel _viewModel;

    #endregion FIELDS

    public EventsPage(EventsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Dispatcher.DispatchAsync(async () =>
        {
            await ((EventsViewModel)BindingContext).NavigatedToCommand.ExecuteAsync(null);
        });
    }
}