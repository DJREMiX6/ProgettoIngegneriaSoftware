using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Application;
using ProgettoIngegneriaSoftware.UI.Services.EventsService;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

public partial class EventsViewModel : BaseViewModel
{

    #region FIELDS

    private readonly IEventsService _eventsService;

    public ObservableCollection<EventModel> Events { get; set; } = new();

    #endregion FIELDS

    #region CTORS

    public EventsViewModel(IEventsService eventsService)
    {
        _eventsService = eventsService;
        Title = "Events";
        Task.WaitAll(GetMonkeyAsync());
    }

    #endregion CTORS

    [RelayCommand]
    private async Task GetMonkeyAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var events = await _eventsService.GetEventsAsync();
            
            Events.Clear();

            foreach (var e in events)
                Events.Add(e);
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

}