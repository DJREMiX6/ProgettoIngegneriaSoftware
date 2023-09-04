using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProgettoIngegneriaSoftware.UI.Models;
using ProgettoIngegneriaSoftware.UI.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Services.EventsService;
using ProgettoIngegneriaSoftware.UI.Views;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

[QueryProperty("OpenEventDetail", "OpenEventDetail")]
[QueryProperty("EventIdToOpenDetail", "EventIdToOpenDetail")]
public partial class EventsViewModel : BaseViewModel
{

    #region FIELDS
    
    private readonly IEventsService _eventsService;

    public ObservableCollection<IDisplayEvent> EventsCollection { get; private set; }

    [ObservableProperty]
    private bool _openEventDetail;

    [ObservableProperty]
    private Guid _eventIdToOpenDetail;

    #endregion FIELDS

    #region CTORS

    public EventsViewModel(IEventsService eventsService)
    {
        _eventsService = eventsService;
        Title = "Events";
        EventsCollection = new();
    }

    #endregion CTORS

    [RelayCommand]
    private async Task Refresh_EventsRefreshView()
    {
        IsBusy = true;
        await GetEventsAsync();
        IsBusy = false;
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
    private async Task OpenQrCodeDetailViewAsync(IDisplayEvent eventModel)
    {
        await Shell.Current.GoToAsync(nameof(QrCodeDetailView), true, new Dictionary<string, object>()
        {
            {"EventId", eventModel.Id}
        });
    }

    [RelayCommand]
    private async Task OpenEventDetailViewAsync(IDisplayEvent eventModel)
    {
        await Shell.Current.GoToAsync(nameof(EventDetailView), true, new Dictionary<string, object>()
        {
            {"EventId", eventModel.Id}
        });
    }

    [RelayCommand]
    private async Task OnNavigatedToAsync()
    {
        if (OpenEventDetail)
        {
            await Shell.Current.GoToAsync(nameof(EventDetailView), true, new Dictionary<string, object>()
            {
                {"EventId", EventIdToOpenDetail}
            });
            OpenEventDetail = false;
            EventIdToOpenDetail = Guid.Empty;
            return;
        }

        IsBusy = false;
        await GetEventsAsync();
    }

    private async Task GetEventsFromServiceAsync()
    {
        EventsCollection.Clear();
        var events = await _eventsService.GetEventsAsync();
        foreach (var displayEvent in events)
        {
            EventsCollection.Add(displayEvent);
        }
    }

}