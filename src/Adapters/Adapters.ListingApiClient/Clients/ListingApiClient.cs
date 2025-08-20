using System.Net.Http.Json;
using Adapters.ListingApiClient.Configuration;
using Adapters.ListingApiClient.Constants;
using Domain.Models;
using Domain.Ports;

namespace Adapters.ListingApiClient.Clients;

internal class ListingApiClient : IListingSearchPort
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ListingApiClientAdapterSettings _settings;

    public ListingApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ListingsResponse> GetListings(string searchQuery, uint page = 1, uint pageSize = 15)
    {
        using var httpClient = _httpClientFactory.CreateClient(ClientConstants.ListingApiClientName);

        return await httpClient.GetFromJsonAsync<ListingsResponse>(
                $"?type=koop&zo={searchQuery}&page={page}&pagesize={pageSize}")
            ?? throw new InvalidOperationException("Empty response body");
    }
}
