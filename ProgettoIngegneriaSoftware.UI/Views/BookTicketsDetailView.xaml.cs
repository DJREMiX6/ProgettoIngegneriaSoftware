using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgettoIngegneriaSoftware.UI.ViewModels;

namespace ProgettoIngegneriaSoftware.UI.Views;

public partial class BookTicketsDetailView : ContentPage
{

    #region FIELDS

    private readonly BookTicketsDetailViewModel _viewModel;

    #endregion FIELDS

    #region CTORS

    public BookTicketsDetailView(BookTicketsDetailViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }

    #endregion CTORS

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Dispatcher.DispatchAsync(async () =>
        {
            await _viewModel.OnNavigatedTo();
        });
    }

    private void SeatZonePicker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        _viewModel.SeatZoneSelectedCommand.Execute(null);
    }

    private void SeatRowPicker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        _viewModel.SeatRowSelectedCommand.Execute(null);
    }

    private void SeatIndexPicker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        _viewModel.SeatIndexSelectedCommand.Execute(null);
    }
}