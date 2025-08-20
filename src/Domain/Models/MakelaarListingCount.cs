namespace Domain.Models;

public record MakelaarListingCount(int MakelaarId, string MakelaarNaam, uint ListingCount)
{
    public MakelaarListingCount incrementListing() => this with { ListingCount = ListingCount + 1 };
}
