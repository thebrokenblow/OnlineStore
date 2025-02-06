using System.Net;

namespace OnlineStore.IntegrationTests.Drivers;

public class ApiClientException(
    HttpStatusCode httpStatus,
    string? message,
    Exception? innerException = null) : Exception($"HTTP status code {httpStatus}: {message}", innerException)
{
    public HttpStatusCode HttpStatusCode { get; } = httpStatus;
}