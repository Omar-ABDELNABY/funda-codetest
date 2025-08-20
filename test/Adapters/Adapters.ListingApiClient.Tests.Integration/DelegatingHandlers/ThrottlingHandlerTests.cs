using System.Net;
using Adapters.ListingApiClient.DelegatingHandlers;

namespace Adapters.ListingApiClient.Tests.Integration.DelegatingHandlers;

public class ThrottlingHandlerTests
{
    [Fact]
    public async Task GivenTooManyRequests_ShouldBeThrotteled()
    {
        // Arrange
        var fakeHandler = new FakeHandler();
        var throttlingHandler = new ThrottlingHandler(fakeHandler);
        var httpClient = new HttpClient(throttlingHandler);

        // Act
        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 0; i < 105; i++)
        {
            tasks.Add(httpClient.GetAsync("http://test/"));
        }

        var responses = await Task.WhenAll(tasks);

        // Assert
        Assert.All(responses, r => Assert.Equal(HttpStatusCode.OK, r.StatusCode));

        var firstBatch = fakeHandler.CallTimes.GetRange(0, 100);
        var secondBatch = fakeHandler.CallTimes.GetRange(100, 5);

        var delayBetweenBatches = secondBatch[0] - firstBatch[^1];
        Assert.True(delayBetweenBatches >= TimeSpan.FromSeconds(60),
            $"Expected remaining requests to wait 1 min, but delay was {delayBetweenBatches.TotalSeconds:F1}s");
    }
}

internal class ThrottlingHandlerTestable : ThrottlingHandler
{
    public ThrottlingHandlerTestable(HttpMessageHandler innerHandler) : base(innerHandler)
    {
    }
}

public class FakeHandler : HttpMessageHandler
{
    private readonly object _lock = new();
    public List<DateTime> CallTimes { get; } = new();

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        lock (_lock)
        {
            CallTimes.Add(DateTime.UtcNow);
        }
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
    }
}
