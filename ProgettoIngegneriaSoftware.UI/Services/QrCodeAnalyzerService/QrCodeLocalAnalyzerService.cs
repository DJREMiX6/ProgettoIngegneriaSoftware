using ProgettoIngegneriaSoftware.UI.Services.EventsService;

namespace ProgettoIngegneriaSoftware.UI.Services.QrCodeAnalyzerService;

public class QrCodeLocalAnalyzerService : IQrCodeAnalyzerService
{

    private readonly IEventsService _eventsService;

    public QrCodeLocalAnalyzerService(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }

    public async Task<bool> IsValidQrCodeResult(string qrCodeResultValue)
    {//TODO
        throw new NotImplementedException();
        /*int id = -1;
        var result = Int32.TryParse(qrCodeResultValue, out id);
        if (id <= -1)
            return false;
        var events = await _eventsService.GetEventsAsync();
        return events.Count(readableEvent => readableEvent.Id == id) == 1;*/
    }

    public async Task<int> GetIdFromQrCodeResult(string qrCodeResultValue) => Int32.Parse(qrCodeResultValue);
}