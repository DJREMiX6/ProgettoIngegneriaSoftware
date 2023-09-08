using ProgettoIngegneriaSoftware.UI.Services.EventsService;

namespace ProgettoIngegneriaSoftware.UI.Services.QrCodeAnalyzerService;

public class QrCodeAnalyzerService : IQrCodeAnalyzerService
{

    private readonly IEventsService _eventsService;

    public QrCodeAnalyzerService(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }

    public async Task<Guid> GetIdFromQrCodeResult(string qrCodeResultValue)
    {
        try
        {
            var qrCodeResultValueParseOperationResult = Guid.TryParse(qrCodeResultValue, out var parsedQrCodeResult);
            if (!qrCodeResultValueParseOperationResult)
                return Guid.Empty;

            var eventFromApi = await _eventsService.GetEventAsync(parsedQrCodeResult);
            return eventFromApi?.Id ?? Guid.Empty;
        }
        catch (Exception ex)
        {
            return Guid.Empty;
        }
        
    }
}