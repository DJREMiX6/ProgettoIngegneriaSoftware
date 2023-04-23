using ProgettoIngegneriaSoftware.UI.ViewModels;

namespace ProgettoIngegneriaSoftware.UI.Views;

public partial class QrCodeDetailView : ContentPage
{
    public QrCodeDetailView(QrCodeDetailViewModel qrCodeDetailViewModel)
    {
        BindingContext = qrCodeDetailViewModel;
        InitializeComponent();
    }

    public int EventId { get; set; }

}