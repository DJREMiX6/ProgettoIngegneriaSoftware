namespace ProgettoIngegneriaSoftware.UI.Services.QrCodeAnalyzerService;

public interface IQrCodeAnalyzerService
{
    public Task<bool> IsValidQrCodeResult(string qrCodeResultValue);

    public Task<int> GetIdFromQrCodeResult(string qrCodeResultValue);
}