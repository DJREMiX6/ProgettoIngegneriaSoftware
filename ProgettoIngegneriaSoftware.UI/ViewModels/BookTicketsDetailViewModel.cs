using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Services.EventsService;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

[QueryProperty("EventId", "EventId")]
public partial class BookTicketsDetailViewModel : BaseViewModel
{

    #region FIELDS

    private readonly IEventsService _eventsService;
    private readonly IEventBookingService _eventBookingService;

    [ObservableProperty]
    private Guid _eventId;

    [ObservableProperty]
    private string? _selectedSeatZone;

    [ObservableProperty]
    private string? _selectedSeatRow;

    [ObservableProperty]
    private int? _selectedSeatIndex;

    [ObservableProperty] 
    private bool _isSeatZoneSelected = false;

    [ObservableProperty] 
    private bool _isSeatRowSelected = false;

    [ObservableProperty] 
    private bool _isSeatIndexSelected = false;

    [ObservableProperty] 
    private bool _isAddButtonEnabled = false;

    [ObservableProperty] 
    private bool _seatsSelected = false;

    public ObservableCollection<ISeatResult> AvailableSeats { get; private set; } = new();

    public ObservableCollection<ISeatResult> SelectedSeats { get; private set; } = new();

    public ObservableCollection<string> AvailableSeatZones { get; private set; } = new();

    public ObservableCollection<string> AvailableSeatRows { get; private set; } = new();

    public ObservableCollection<int> AvailableSeatIndexes { get; private set; } = new();

    #endregion FIELDS

    #region CTORS

    public BookTicketsDetailViewModel(IEventsService eventsService, IEventBookingService eventBookingService)
    {
        _eventsService = eventsService;
        _eventBookingService = eventBookingService;
    }

    #endregion CTORS

    #region METHODS

    private async Task LoadBookableSeats()
    {
        var displayEvent = await _eventsService.GetEventAsync(EventId);

        if (displayEvent == null)
        {
            await Shell.Current.GoToAsync("..");
            await Shell.Current.DisplayAlert("Error!", "There was an error loading the Event data.", "Ok");
        }
        else
        {
            UpdateAvailableSeats(displayEvent);
            UpdateAvailableSeatZoneNames();
        }
    }

    public async Task OnNavigatedTo()
    {
        IsBusy = true;
        await LoadBookableSeats();
        IsBusy = false;
    }

    private void UpdateAvailableSeats(IDisplayEvent displayEvent)
    {
        AvailableSeats.Clear();
        foreach (var availableSeat in displayEvent.AvailableSeats)
        {
            AvailableSeats.Add(availableSeat);
        }
    }

    private void UpdateAvailableSeatZoneNames()
    {
        AvailableSeatZones.Clear();

        var availableSeatZonesNames = AvailableSeats
            .Select(seatResult => seatResult.SeatZoneName)
            .Distinct();

        foreach (var seatZoneName in availableSeatZonesNames)
            AvailableSeatZones.Add(seatZoneName);
    }

    private void UpdateAvailableSeatRowNames()
    {
        AvailableSeatRows.Clear();
        var availableSeatRowsNames = AvailableSeats
            .Where(seatResult => seatResult.SeatZoneName == SelectedSeatZone)
            .Select(seatResult => seatResult.SeatRowName)
            .Distinct();

        foreach (var availableSeatRowName in availableSeatRowsNames)
            AvailableSeatRows.Add(availableSeatRowName);
    }

    private void UpdateAvailableSeatIndexes()
    {
        AvailableSeatIndexes.Clear();
        var availableSeatIndexesNames = AvailableSeats
            .Where(seatResult => seatResult.SeatZoneName == SelectedSeatZone && seatResult.SeatRowName == SelectedSeatRow)
            .Select(seatResult => seatResult.SeatIndex);

        foreach (var availableSeatIndexes in availableSeatIndexesNames)
            AvailableSeatIndexes.Add(availableSeatIndexes);
    }

    private void ResetSelectedSeatData()
    {
        SelectedSeatIndex = null;
        SelectedSeatRow = null;
        SelectedSeatZone = null;

        IsSeatRowSelected = false;
        IsSeatZoneSelected = false;
        IsSeatIndexSelected = false;

        IsAddButtonEnabled = false;
        SeatsSelected = false;
    }

    private void UpdateAddButtonState() =>
        IsAddButtonEnabled = IsSeatZoneSelected && IsSeatRowSelected && IsSeatIndexSelected;

    #endregion METHODS

    #region COMMANDS

    [RelayCommand]
    private void SeatZoneSelected()
    {
        IsSeatZoneSelected = SelectedSeatZone != null;
        UpdateAvailableSeatRowNames();
        UpdateAddButtonState();
    }

    [RelayCommand]
    private void SeatRowSelected()
    {
        IsSeatRowSelected = SelectedSeatRow != null;
        UpdateAvailableSeatIndexes();
        UpdateAddButtonState();
    }

    [RelayCommand]
    private void SeatIndexSelected()
    {
        IsSeatIndexSelected = SelectedSeatIndex != null;
        UpdateAddButtonState();
    }

    [RelayCommand]
    private async Task AddTicketToSelected()
    {

        if (!IsSeatZoneSelected || !IsSeatRowSelected || !IsSeatIndexSelected)
            return;

        var selectedSeat = AvailableSeats.FirstOrDefault(seatResult =>
            seatResult.SeatZoneName == SelectedSeatZone && seatResult.SeatRowName == SelectedSeatRow &&
            seatResult.SeatIndex == SelectedSeatIndex);

        if (selectedSeat == null)
            await Shell.Current.DisplayAlert("Error!", "Invalid selected ticket.", "Ok");
        else
        {
            SelectedSeats.Add(selectedSeat);
            AvailableSeats.Remove(selectedSeat);
            SeatsSelected = true;
        }

        ResetSelectedSeatData();
        UpdateAvailableSeatZoneNames();
    }

    [RelayCommand]
    private async Task BookTickets()
    {
        if (!SeatsSelected)
            return;

        try
        {
            await _eventBookingService.BookTicket(_eventId!, SelectedSeats.Select(seatResult => seatResult.SeatId).ToArray());
            ResetSelectedSeatData();
            await Shell.Current.GoToAsync("..", animate: true);
        }
        catch (Exception ex)
        {

        }
    }

    [RelayCommand]
    private void RemoveSeatFromSelected(ISeatResult seat)
    {
        SelectedSeats.Remove(seat);
        AvailableSeats.Add(seat);
        
        UpdateAvailableSeatZoneNames();
        UpdateAvailableSeatRowNames();
        UpdateAvailableSeatIndexes();
    }

    #endregion COMMANDS

}