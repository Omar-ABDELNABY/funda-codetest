using Domain.Models;

namespace Domain.Ports;

public interface IListingSearchPort
{
    public Task<ListingsResponse> GetListings(string searchQuery, uint page = 1, uint pageSize = 15);
}
