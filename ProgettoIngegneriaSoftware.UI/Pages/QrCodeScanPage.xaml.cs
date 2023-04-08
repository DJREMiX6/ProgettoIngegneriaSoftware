using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Net.Maui.Controls;
using ZXing.Net.Maui;

namespace ProgettoIngegneriaSoftware.UI.Pages;

public partial class QrCodeScanPage : ContentPage
{
    public QrCodeScanPage()
    {
        InitializeComponent();
        cameraBarcodeReaderView.Options = new BarcodeReaderOptions()
        {
            Formats = BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = false,
            TryHarder = true,
            TryInverted = true
        };
    }

    protected void CameraBarcodeReaderView_OnBarcodesDetected(object? sender, BarcodeDetectionEventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            foreach (var barcodeResult in e.Results)
                cameraBarcodeReaderResultLbl.Text =
                    $"Format: {barcodeResult.Format}{Environment.NewLine} Value: {barcodeResult.Value}{Environment.NewLine}";
        });
    }
}