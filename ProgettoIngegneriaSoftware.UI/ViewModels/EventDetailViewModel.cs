using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProgettoIngegneriaSoftware.UI.Models;
using ProgettoIngegneriaSoftware.UI.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Services.EventsService;
using ProgettoIngegneriaSoftware.UI.Views;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

[QueryProperty("EventId", "EventId")]
public partial class EventDetailViewModel : BaseViewModel
{

    [ObservableProperty]
    private IDisplayEvent? _eventModelToDetail;

    [ObservableProperty]
    private Guid _eventId;

    private readonly IEventsService _eventsService;

    public EventDetailViewModel(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }
    
    public async Task GetEventFromServiceAsync()
    {
        var eventModel = await _eventsService.GetEventAsync(EventId);

        if (eventModel == null)
        {
            await Shell.Current.GoToAsync("..");
            await Shell.Current.DisplayAlert("Error!", "There was an error loading the Event.", "Ok");
        }
        else
        {
            EventModelToDetail = eventModel;
        }
    }

    public async Task OnNavigatedTo()
    {
        IsBusy = true;
        await GetEventFromServiceAsync();
        IsBusy = false;
    }

    [RelayCommand]
    private async Task NavigateToBookTickets()
    {
        await Shell.Current.GoToAsync(nameof(BookTicketsDetailView), animate: true, new Dictionary<string, object>()
        {
            {"EventId", EventId}
        });
    }

    [RelayCommand]
    private async Task NavigateToBookedTickets()
    {
        await Shell.Current.GoToAsync(nameof(BookedTicketsDetailView), animate: true, new Dictionary<string, object>()
        {
            {"EventId", EventId}
        });
    }

}