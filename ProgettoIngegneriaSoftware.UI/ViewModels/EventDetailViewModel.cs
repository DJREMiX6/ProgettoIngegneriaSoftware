using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProgettoIngegneriaSoftware.UI.Models;
using ProgettoIngegneriaSoftware.UI.Services.EventsService;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

[QueryProperty("EventId", "EventId")]
[QueryProperty("EventModelToDetail", "EventModelToDetail")]
[QueryProperty("ToFollow", "ToFollow")]
public partial class EventDetailViewModel : BaseViewModel
{

    [ObservableProperty]
    private List<ReadableEventModel> _eventWrapper;

    [ObservableProperty]
    private ReadableEventModel? _eventModelToDetail;

    [ObservableProperty]
    private bool _toFollow;

    [ObservableProperty]
    private int _eventId;

    private readonly IEventsService _eventsService;

    public EventDetailViewModel(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }

    [RelayCommand]
    private async Task ClickedOnEventAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var madeChanges = false;
            EventId = EventModelToDetail.Id;

            if (EventModelToDetail.IsBookedByCurrentUser)
            {
                await UnFollowAsync(EventModelToDetail);
                madeChanges = true;
            }
            else if (!EventModelToDetail.IsBookedByCurrentUser && EventModelToDetail.IsBookable)
            {
                await FollowAsync(EventModelToDetail);
                madeChanges = true;
            }

            if (madeChanges)
                await GetEventFromServiceAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", "Error with this action.", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GetEventFromServiceAsync()
    {
        var eventModel = await _eventsService.GetEventAsync(EventId);
        if (eventModel == null)
        {
            await Shell.Current.GoToAsync("..");
            await Shell.Current.DisplayAlert("Error!", "There was an error loading the Event.", "Ok");
        }
        else
        {
            EventWrapper = new List<ReadableEventModel>();
            EventModelToDetail = new ReadableEventModel();
            EventModelToDetail = eventModel;
            EventId = eventModel.Id;
            EventWrapper.Add(EventModelToDetail);
        }
    }

    [RelayCommand]
    private void OnNavigatedTo()
    {
        if (EventId > 0)
            GetEventFromServiceAsync().Wait();
        else
        {
            EventId = EventModelToDetail!.Id;
        }

        EventWrapper = new List<ReadableEventModel> { EventModelToDetail! };

        if (ToFollow)
        {
            if(!EventModelToDetail!.IsBookedByCurrentUser)
            {
                FollowAsync(EventModelToDetail!).Wait();
                GetEventFromServiceAsync().Wait();
            }
            else
            {
                Shell.Current.DisplayAlert("Attention!", "You are already following this event.", "Ok");
            }
        }
    }

    private async Task UnFollowAsync(ReadableEventModel eventModel)
    {
        await _eventsService.UnFollowEventAsync(eventModel.Id);
    }

    private async Task FollowAsync(ReadableEventModel eventModel)
    {
        await _eventsService.FollowEventAsync(eventModel.Id);
    }

}