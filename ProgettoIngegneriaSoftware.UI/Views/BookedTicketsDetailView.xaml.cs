using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgettoIngegneriaSoftware.UI.ViewModels;

namespace ProgettoIngegneriaSoftware.UI.Views;

public partial class BookedTicketsDetailView : ContentPage
{

    #region FIELDS

    private readonly BookedTicketsDetailViewModel _viewModel;

    #endregion FIELDS

    #region CTORS

    public BookedTicketsDetailView(BookedTicketsDetailViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }

    #endregion CTORS

    #region METHODS

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Dispatcher.DispatchAsync(async () => await _viewModel.OnNavigatedTo());
    }

    #endregion METHODS

}