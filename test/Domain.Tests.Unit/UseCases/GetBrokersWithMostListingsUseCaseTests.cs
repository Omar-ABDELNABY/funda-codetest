using AutoFixture.Xunit2;
using Domain.Models;
using Domain.Ports;
using Domain.UseCases;
using Microsoft.Extensions.Logging;
using Moq;

namespace Domain.Tests.Unit.UseCases;

public class GetBrokersWithMostListingsUseCaseTests
{
    private const uint PageSize = 100;
    private const int BrokerId1 = 1;
    private const string BrokerName1 = "Broker A";
    private const int BrokerId2 = 2;
    private const string BrokerName2 = "Broker B";
    private const int BrokerId3 = 3;
    private const string BrokerName3 = "Broker C";

    private readonly Mock<IListingSearchPort> _listingSearchPortMock = new(MockBehavior.Strict);
    private readonly Mock<ILogger<GetBrokersWithMostListingsUseCase>> _loggerMock = new(MockBehavior.Loose);
    private readonly GetBrokersWithMostListingsUseCase _sut;

    public GetBrokersWithMostListingsUseCaseTests()
    {
        _sut = new GetBrokersWithMostListingsUseCase(_listingSearchPortMock.Object, _loggerMock.Object);
    }

    [Theory]
    [InlineAutoData(false, "/amsterdam/")]
    [InlineAutoData(true, "/amsterdam/tuin/")]
    public async Task WhenGettingBrokersWithMostListingsInAmsterdam_CallsThePortWithCorrespondingSearchQuery(
        bool withGarden,
        string searchQuery,
        ListingsResponse listingsResponse)
    {
        // Arrange
        listingsResponse = listingsResponse with { Paging = new Paging(1, 1, string.Empty, string.Empty) };
        _listingSearchPortMock
            .Setup(x => x.GetListings(searchQuery, 1, PageSize))
            .ReturnsAsync(listingsResponse);

        // Act
        var result = await _sut.GetBrokersWithMostListingsInAmsterdam(withGarden);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<MakelaarListingCount>>(result);
        _listingSearchPortMock.Verify(x => x.GetListings(searchQuery, 1, 100), Times.Once);
    }

    [Fact]
    public async Task GivenEmptyListingsResponse_WhenGettingBrokersWithMostListingsInAmsterdam_ReturnsEmptyList()
    {
        // Arrange
        var listingsResponse = new ListingsResponse()
        {
            Paging = new Paging(1, 1, string.Empty, string.Empty),
        };

        _listingSearchPortMock
            .Setup(x => x.GetListings("/amsterdam/", 1, PageSize))
            .ReturnsAsync(listingsResponse);

        // Act
        var result = await _sut.GetBrokersWithMostListingsInAmsterdam();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GivenListingsResponseWithMultipleBrokers_WhenGettingBrokersWithMostListingsInAmsterdam_ReturnsOrderedList()
    {
        // Arrange

        var listingsResponse = new ListingsResponse()
        {
            Paging = new(1, 1, String.Empty, string.Empty),
            Objects = new List<ListingObject>
            {
                new() { MakelaarId = BrokerId1, MakelaarNaam = BrokerName1 },
                new() { MakelaarId = BrokerId2, MakelaarNaam = BrokerName2 },
                new() { MakelaarId = BrokerId1, MakelaarNaam = BrokerName1 },
                new() { MakelaarId = BrokerId3, MakelaarNaam = BrokerName3 },
            }
        };

        _listingSearchPortMock
            .Setup(x => x.GetListings("/amsterdam/", 1, PageSize))
            .ReturnsAsync(listingsResponse);

        var expectedFirst = new MakelaarListingCount(BrokerId1, BrokerName1, 2);
        var expected2 = new MakelaarListingCount(BrokerId2, BrokerName2, 1);
        var expected3 = new MakelaarListingCount(BrokerId3, BrokerName3, 1);

        // Act
        var result = await _sut.GetBrokersWithMostListingsInAmsterdam();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.Equal(expectedFirst, result.First());
        Assert.Contains(result, b => b == expected2);
        Assert.Contains(result, b => b == expected3);
    }

    [Fact]
    public async Task GivenMultiplePagesOfListingsResponseWithMultipleBrokers_WhenGettingBrokersWithMostListingsInAmsterdam_ReturnsOrderedList()
    {
        // Arrange

        var listingsResponse1 = new ListingsResponse()
        {
            Paging = new(3, 1, String.Empty, string.Empty),
            Objects = new List<ListingObject>
            {
                new() { MakelaarId = BrokerId1, MakelaarNaam = BrokerName1 },
                new() { MakelaarId = BrokerId2, MakelaarNaam = BrokerName2 },
                new() { MakelaarId = BrokerId3, MakelaarNaam = BrokerName3 },
            }
        };

        var listingsResponse2 = new ListingsResponse()
        {
            Paging = new(3, 2, String.Empty, string.Empty),
            Objects = new List<ListingObject>
            {
                new() { MakelaarId = BrokerId2, MakelaarNaam = BrokerName2 },
                new() { MakelaarId = BrokerId3, MakelaarNaam = BrokerName3 },
            }
        };

        var listingsResponse3 = new ListingsResponse()
        {
            Paging = new(3, 3, String.Empty, string.Empty),
            Objects = new List<ListingObject>
            {
                new() { MakelaarId = BrokerId1, MakelaarNaam = BrokerName1 },
                new() { MakelaarId = BrokerId1, MakelaarNaam = BrokerName1 },
            }
        };

        _listingSearchPortMock
            .Setup(x => x.GetListings("/amsterdam/", 1, PageSize))
            .ReturnsAsync(listingsResponse1);
        _listingSearchPortMock
            .Setup(x => x.GetListings("/amsterdam/", 2, PageSize))
            .ReturnsAsync(listingsResponse2);
        _listingSearchPortMock
            .Setup(x => x.GetListings("/amsterdam/", 3, PageSize))
            .ReturnsAsync(listingsResponse3);


        var expectedFirst = new MakelaarListingCount(BrokerId1, BrokerName1, 3);
        var expected2 = new MakelaarListingCount(BrokerId2, BrokerName2, 2);
        var expected3 = new MakelaarListingCount(BrokerId3, BrokerName3, 2);

        // Act
        var result = await _sut.GetBrokersWithMostListingsInAmsterdam();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.Equal(expectedFirst, result.First());
        Assert.Contains(result, b => b == expected2);
        Assert.Contains(result, b => b == expected3);
    }
}
