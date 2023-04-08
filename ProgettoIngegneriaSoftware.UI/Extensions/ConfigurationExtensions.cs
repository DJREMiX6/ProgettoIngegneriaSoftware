using Microsoft.Extensions.Configuration;
using ProgettoIngegneriaSoftware.UI.Models;

namespace ProgettoIngegneriaSoftware.UI.Extensions;

internal static class ConfigurationExtensions
{

    private const string ENDPOINTS_SECTION_KEY = "Endpoints";
    private const string API_SECTION_KEY = "api";
    private const string LOGIN_REGISTER_SECTION_KEY = "loginregister";
    private const string USER_SECTION_KEY = "user";
    private const string EVENTS_SECTION_KEY = "events";

    private const string ENDPOINT_NAME_KEY = "Name";
    private const string ENDPOINT_ENDPOINT_KEY = "Endpoint";
    private const string ENDPOINT_HTTP_METHOD_KEY = "HttpMethod";

    private static IConfigurationSection GetEndpointsSection(this IConfiguration configuration) =>
        configuration.GetSection(ENDPOINTS_SECTION_KEY);

    private static IConfigurationSection GetApiEndpointSection(this IConfiguration configuration) =>
        configuration.GetEndpointsSection().GetSection(API_SECTION_KEY);

    private static IConfigurationSection GetLoginRegisterSection(this IConfiguration configuration) =>
        configuration.GetApiEndpointSection().GetSection(LOGIN_REGISTER_SECTION_KEY);

    private static IConfigurationSection GetUserSection(this IConfiguration configuration) =>
        configuration.GetApiEndpointSection().GetSection(USER_SECTION_KEY);

    private static IConfigurationSection GetEventsSection(this IConfiguration configuration) =>
        configuration.GetApiEndpointSection().GetSection(EVENTS_SECTION_KEY);

    internal static IEnumerable<EndpointInfo> GetLoginRegisterEndpoints(this IConfiguration configuration)
    {
        return configuration.GetLoginRegisterSection().GetChildren().Select(section =>
        {
            if (section[ENDPOINT_NAME_KEY] == null)
                throw new ArgumentException($"The argument {ENDPOINT_NAME_KEY} is null.");
            if (section[ENDPOINT_ENDPOINT_KEY] == null)
                throw new ArgumentException($"The argument {ENDPOINT_ENDPOINT_KEY} is null.");
            if (section[ENDPOINT_HTTP_METHOD_KEY] == null)
                throw new ArgumentException($"The argument {ENDPOINT_HTTP_METHOD_KEY} is null.");


            return new EndpointInfo(
                LOGIN_REGISTER_SECTION_KEY,
                section["Name"],
                section["Endpoint"],
                section["HttpMethod"]);
        });
    }

    internal static IEnumerable<EndpointInfo> GetUserEndpoints(this IConfiguration configuration)
    {
        return configuration.GetUserSection().GetChildren().Select(section =>
        {
            if (section[ENDPOINT_NAME_KEY] == null)
                throw new ArgumentException($"The argument {ENDPOINT_NAME_KEY} is null.");
            if (section[ENDPOINT_ENDPOINT_KEY] == null)
                throw new ArgumentException($"The argument {ENDPOINT_ENDPOINT_KEY} is null.");
            if (section[ENDPOINT_HTTP_METHOD_KEY] == null)
                throw new ArgumentException($"The argument {ENDPOINT_HTTP_METHOD_KEY} is null.");


            return new EndpointInfo(
                USER_SECTION_KEY,
                section["Name"],
                section["Endpoint"],
                section["HttpMethod"]);
        });
    }

    internal static IEnumerable<EndpointInfo> GetEventsEndpoints(this IConfiguration configuration)
    {
        return configuration.GetEventsSection().GetChildren().Select(section =>
        {
            if (section[ENDPOINT_NAME_KEY] == null)
                throw new ArgumentException($"The argument {ENDPOINT_NAME_KEY} is null.");
            if (section[ENDPOINT_ENDPOINT_KEY] == null)
                throw new ArgumentException($"The argument {ENDPOINT_ENDPOINT_KEY} is null.");
            if (section[ENDPOINT_HTTP_METHOD_KEY] == null)
                throw new ArgumentException($"The argument {ENDPOINT_HTTP_METHOD_KEY} is null.");


            return new EndpointInfo(
                EVENTS_SECTION_KEY,
                section["Name"],
                section["Endpoint"],
                section["HttpMethod"]);
        });
    }

}