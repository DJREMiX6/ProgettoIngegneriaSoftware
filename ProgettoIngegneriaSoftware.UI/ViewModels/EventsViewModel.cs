using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProgettoIngegneriaSoftware.UI.Models;
using ProgettoIngegneriaSoftware.UI.Services.EventsService;
using ProgettoIngegneriaSoftware.UI.Views;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

[QueryProperty("OpenEventDetail", "OpenEventDetail")]
[QueryProperty("EventIdToOpenDetail", "EventIdToOpenDetail")]
[QueryProperty("ToFollow", "ToFollow")]
public partial class EventsViewModel : BaseViewModel
{

    #region FIELDS

    private readonly IEventsService _eventsService;

    [ObservableProperty]
    private List<ReadableEventModel> _events;

    [ObservableProperty]
    private bool _openEventDetail;

    [ObservableProperty]
    private int _eventIdToOpenDetail;

    [ObservableProperty]
    private bool _toFollow;

    #endregion FIELDS

    #region CTORS

    public EventsViewModel(IEventsService eventsService)
    {
        _eventsService = eventsService;
        Title = "Events";
    }

    #endregion CTORS

    [RelayCommand]
    private async Task Refresh_EventsRefreshView(RefreshView refreshView)
    {
        refreshView.IsRefreshing = true;
        await GetEventsAsync();
        refreshView.IsRefreshing = false;
    }

    [RelayCommand]
    private async Task GetEventsAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            await GetEventsFromServiceAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", ex.Message, "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ClickedOnEventAsync(ReadableEventModel eventModel)
    {
        if(IsBusy) return;

        try
        {
            IsBusy = true;
            var madeChanges = false;

            if (eventModel.IsBookedByCurrentUser)
            {
                await UnFollowAsync(eventModel);
                madeChanges = true;
            }
            else if (!eventModel.IsBookedByCurrentUser && eventModel.IsBookable)
            {
                await FollowAsync(eventModel);
                madeChanges = true;
            }

            if (madeChanges)
                await GetEventsFromServiceAsync();
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
    private async Task OpenQrCodeDetailViewAsync(ReadableEventModel readableEventModel)
    {
        await Shell.Current.GoToAsync(nameof(QrCodeDetailView), true, new Dictionary<string, object>()
        {
            {"EventId", readableEventModel.Id}
        });
    }

    [RelayCommand]
    private async Task OpenEventDetailViewAsync(ReadableEventModel readableEventModel)
    {
        await Shell.Current.GoToAsync(nameof(EventDetailView), true, new Dictionary<string, object>()
        {
            {"EventModelToDetail", readableEventModel}
        });
    }

    [RelayCommand]
    private async Task OnNavigatedToAsync()
    {
        if (OpenEventDetail)
        {
            await Shell.Current.GoToAsync(nameof(EventDetailView), true, new Dictionary<string, object>()
            {
                {"EventId", EventIdToOpenDetail},
                {"ToFollow", ToFollow}
            });
            OpenEventDetail = false;
            EventIdToOpenDetail = 0;
            ToFollow = false;
            return;
        }

        IsBusy = false;
        await GetEventsAsync();
    }

    private async Task UnFollowAsync(ReadableEventModel eventModel)
    {
        await _eventsService.UnFollowEventAsync(eventModel.Id);
    }

    private async Task FollowAsync(ReadableEventModel eventModel)
    {
        await _eventsService.FollowEventAsync(eventModel.Id);
    }

    private async Task GetEventsFromServiceAsync()
    {
        Events = new List<ReadableEventModel>();
        var events = await _eventsService.GetEventsAsync();
        Events.AddRange(events);
    }

}