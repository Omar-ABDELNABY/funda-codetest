using Domain.Models;

namespace Domain.UseCases;

public interface IGetBrokersWithMostListingsUseCase
{
    Task<IReadOnlyCollection<MakelaarListingCount>> GetBrokersWithMostListingsInAmsterdam(bool withGarden = false);
}
