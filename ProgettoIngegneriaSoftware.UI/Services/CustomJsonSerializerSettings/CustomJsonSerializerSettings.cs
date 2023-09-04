using Newtonsoft.Json;
using ProgettoIngegneriaSoftware.UI.Helpers;

namespace ProgettoIngegneriaSoftware.UI.Services.CustomJsonSerializerSettings;

public class CustomJsonSerializerSettings : JsonSerializerSettings
{
    public CustomJsonSerializerSettings()
    {
        SetUpConverters();
    }

    private void SetUpConverters()
    {
        Converters.Add(new EventResultJsonConverter());
    }

}