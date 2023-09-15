using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Services.EventsService;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

[QueryProperty("EventId", "EventId")]
public partial class BookedTicketsDetailViewModel : BaseViewModel
{
    #region FIELDS

    private readonly IEventsService _eventsService;
    private readonly IEventBookingService _eventBookingService;

    [ObservableProperty]
    private Guid _eventId;

    public ObservableCollection<ISeatResult> BookedSeats { get; private set; } = new();

    #endregion FIELDS

    #region CTORS

    public BookedTicketsDetailViewModel(IEventBookingService eventBookingService, IEventsService eventsService)
    {
        _eventBookingService = eventBookingService;
        _eventsService = eventsService;
    }

    #endregion CTORS

    #region METHODS

    public async Task OnNavigatedTo()
    {
        await LoadBookedSeats();
    }

    private async Task LoadBookedSeats()
    {
        IsBusy = true;

        var displayEvent = await _eventsService.GetEventAsync(EventId);

        if (displayEvent == null)
        {
            await Shell.Current.GoToAsync("..");
            await Shell.Current.DisplayAlert("Error!", "There was an error loading the Event data.", "Ok");
        }
        else
        {
            UpdateBookedSeats(displayEvent);
        }

        IsBusy = false;
    }

    private void UpdateBookedSeats(IDisplayEvent displayEvent)
    {
        BookedSeats.Clear();
        foreach(var seat in displayEvent.BookedSeats) 
            BookedSeats.Add(seat);
    }

    #endregion METHODS

    #region COMMANDS

    [RelayCommand]
    private async Task RemoveBookedSeat(ISeatResult seat)
    {
        try
        {
            await _eventBookingService.CancelBookedTickets(EventId, seat.SeatId);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", "There was an error while removing the booked ticket.", "Ok");
        }
        finally
        {
            await LoadBookedSeats();
        }
    }

    #endregion COMMANDS
}