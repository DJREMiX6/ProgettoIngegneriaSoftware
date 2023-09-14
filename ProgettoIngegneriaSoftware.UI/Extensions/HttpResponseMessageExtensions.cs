using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace ProgettoIngegneriaSoftware.UI.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<ProblemDetails> ReadProblemDetailsAsync(this HttpResponseMessage httpResponseMessage) =>
        await httpResponseMessage.Content.ReadFromJsonAsync<ProblemDetails>();
}