namespace OnlineStore.IntegrationTests.Fixture;

public interface ITestServerFixture
{
    public HttpClient HttpClient { get; }
}