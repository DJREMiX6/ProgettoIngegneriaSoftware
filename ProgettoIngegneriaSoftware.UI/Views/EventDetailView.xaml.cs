

using ProgettoIngegneriaSoftware.UI.Models;
using ProgettoIngegneriaSoftware.UI.ViewModels;

namespace ProgettoIngegneriaSoftware.UI.Views;

public partial class EventDetailView : ContentPage
{

    private readonly EventDetailViewModel _viewModel;

    public EventDetailView(EventDetailViewModel eventDetailViewModel)
    {
        _viewModel = eventDetailViewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }

    public async Task LoadData()
    {
        await _viewModel.OnNavigatedTo();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Dispatcher.DispatchAsync(async () =>
        {
            await _viewModel.OnNavigatedTo();
        });
    }
}