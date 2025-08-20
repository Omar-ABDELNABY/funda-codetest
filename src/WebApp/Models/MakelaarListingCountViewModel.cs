using Domain.Models;

namespace funda_codetest_webapp.Models;

public record MakelaarListingCountViewModel(int MakelaarId, string MakelaarNaam, uint ListingCount)
{
    public static MakelaarListingCountViewModel FromDomain(MakelaarListingCount domain) =>
        new(domain.MakelaarId, domain.MakelaarNaam, domain.ListingCount);
}
