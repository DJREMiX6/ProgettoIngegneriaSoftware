using AndroidX.Navigation;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace ProgettoIngegneriaSoftware.UI.Pages;

public partial class TestPage : ContentPage
{

    private readonly RegisterPage _registerPage;

    public TestPage(RegisterPage registerPage)
    {
        _registerPage = registerPage;
        InitializeComponent();
    }
    
    private async void OpenQrCodeScanPageAsModalBtn_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new QrCodeScanPage(), true);
    }

    private async void OpenRegisterPageBtn_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(_registerPage, true);
    }
}