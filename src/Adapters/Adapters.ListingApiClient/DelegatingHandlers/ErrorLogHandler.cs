using Microsoft.Extensions.Logging;

namespace Adapters.ListingApiClient.DelegatingHandlers;

internal class ErrorLogHandler : DelegatingHandler
{
    private readonly ILogger<ErrorLogHandler> _logger;

    public ErrorLogHandler(ILogger<ErrorLogHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Sending request to {Url}", request.RequestUri);

            var response =  await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogDebug("Request to {Url} succeeded with status code {StatusCode}",
                    request.RequestUri, response.StatusCode);
            }
            else
            {
                _logger.LogError("Request to {Url} failed with status code {StatusCode}",
                    request.RequestUri, response.StatusCode);
            }

            return response;

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while sending request to {Url}", request.RequestUri);
            throw;
        }
    }
}
