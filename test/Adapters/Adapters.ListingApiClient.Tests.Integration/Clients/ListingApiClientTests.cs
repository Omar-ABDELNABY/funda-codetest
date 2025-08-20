using Domain.Ports;

namespace Adapters.ListingApiClient.Tests.Integration.Clients;

public class ListingApiClientTests : TestBase
{
    [Fact]
    public async Task GivenValidSearchQuery_WhenGetListings_ThenReturnsListingsResponse()
    {
        // Arrange
        string searchQuery = "/amsterdam/";
        uint page = 1;
        uint pageSize = 15;

        var sut = GetService<IListingSearchPort>();

        // Act
        var response = await sut.GetListings(searchQuery, page, pageSize);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response.Objects);
    }
}
