using Domain.Models;
using Domain.Ports;
using Microsoft.Extensions.Logging;

namespace Domain.UseCases;

public class GetBrokersWithMostListingsUseCase : IGetBrokersWithMostListingsUseCase
{
    private const string SearchQueryWithGarden = "/amsterdam/tuin/";
    private const string SearchQueryWithoutGarden = "/amsterdam/";
    private const uint PageSize = 100;

    private readonly IListingSearchPort _listingSearchPort;
    private readonly ILogger<GetBrokersWithMostListingsUseCase> _logger;

    public GetBrokersWithMostListingsUseCase(IListingSearchPort listingSearchPort, ILogger<GetBrokersWithMostListingsUseCase> logger)
    {
        _listingSearchPort = listingSearchPort;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<MakelaarListingCount>> GetBrokersWithMostListingsInAmsterdam(bool withGarden = false)
    {
        var isLastPage = false;
        uint page = 1;
        var searchQuery = withGarden ? SearchQueryWithGarden : SearchQueryWithoutGarden;
        var brokersCount = new Dictionary<int, MakelaarListingCount>();

        while (!isLastPage)
        {
            var response = await _listingSearchPort.GetListings(searchQuery, page, PageSize);

            _logger.LogInformation("Fetched page {Page} out of {NumberOfPages} with {Count} listings",
                page, response.Paging.AantalPaginas, response.Objects.Count);

            foreach (var obj in response.Objects)
            {
                if (brokersCount.ContainsKey(obj.MakelaarId))
                {
                    brokersCount[obj.MakelaarId] = brokersCount[obj.MakelaarId].incrementListing();
                }
                else
                {
                    brokersCount[obj.MakelaarId] = new(obj.MakelaarId, obj.MakelaarNaam, 1);
                }
            }

            page++;
            isLastPage = response.Paging.HuidigePagina >= response.Paging.AantalPaginas;
        }

        return brokersCount
            .Select(x => x.Value)
            .OrderByDescending(x => x.ListingCount)
            .ToList();
    }
}
