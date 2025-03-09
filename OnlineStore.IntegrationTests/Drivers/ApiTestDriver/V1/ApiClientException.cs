using System.Net;

namespace OnlineStore.IntegrationTests.Drivers.ApiTestDriver.V1;

public class ApiClientException(HttpStatusCode code, string? message, Exception? innerException = null)
    : Exception($"HTTP status code {code}: {message}", innerException)
{
    public HttpStatusCode HttpStatusCode { get; } = code;
}