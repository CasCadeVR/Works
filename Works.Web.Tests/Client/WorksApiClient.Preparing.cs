using System.Text;

namespace CasCadeVR.Works.Web.Tests.Client;

/// <summary>
/// Имитация клиента для Api запросов
/// </summary>
partial class WorksApiClient
{
    private Task PrepareRequestAsync(HttpClient client_, HttpRequestMessage request_, string? urlBuilder_, CancellationToken cancellationToken)
        => Task.CompletedTask;

    private Task PrepareRequestAsync(HttpClient client_, HttpRequestMessage request_, StringBuilder urlBuilder_, CancellationToken cancellationToken)
        => Task.CompletedTask;

    private Task ProcessResponseAsync(HttpClient client_, HttpResponseMessage request_, CancellationToken cancellationToken)
        => Task.CompletedTask;
}