namespace Domain.Models;

public record ListingsResponse
{
    public int AccountStatus { get; init; }
    public bool EmailNotConfirmed { get; init; }
    public bool ValidationFailed { get; init; }
    public string? ValidationReport { get; init; }
    public int Website { get; init; }
    public Metadata Metadata { get; init; }
    public IReadOnlyCollection<ListingObject> Objects { get; init; } = new List<ListingObject>();
    public Paging Paging { get; init; }
    public int TotaalAantalObjecten { get; init; }
}
