namespace ProgettoIngegneriaSoftware.UI.Services.QrCodeAnalyzerService;

public interface IQrCodeAnalyzerService
{
    public Task<Guid> GetIdFromQrCodeResult(string qrCodeResultValue);
}