namespace Adapters.ListingApiClient.DelegatingHandlers;

internal class ThrottlingHandler : DelegatingHandler
{
    private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(100, 100);
    private static readonly TimeSpan ResetPeriod = TimeSpan.FromMinutes(1);
    private static Timer _resetTimer;

    static ThrottlingHandler()
    {
        _resetTimer = new Timer(ResetSemaphore, null, ResetPeriod, ResetPeriod);
    }

    // Default constructor for IHttpClientFactory
    public ThrottlingHandler() { }

    // Constructor for testing, allows a custom inner handler
    public ThrottlingHandler(HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await Semaphore.WaitAsync(cancellationToken);
        return await base.SendAsync(request, cancellationToken);
    }


    private static void ResetSemaphore(object? state)
    {
        var currentCount = Semaphore.CurrentCount;
        if (currentCount < 100)
        {
            Semaphore.Release(100 - currentCount);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Semaphore.Dispose();
            _resetTimer.Dispose();
        }
        base.Dispose(disposing);
    }
}
