using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProgettoIngegneriaSoftware.UI.Models;
using ProgettoIngegneriaSoftware.UI.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Services.EventsService;

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
        try
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
        catch (Exception ex)
        {

        }
        
    }

    public async Task OnNavigatedTo()
    {
        IsBusy = true;
        await GetEventFromServiceAsync();
        IsBusy = false;
    }

}