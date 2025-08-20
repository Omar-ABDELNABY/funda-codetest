namespace Domain.Models;

public record Project
{
    public int? AantalKamersTotEnMet { get; init; }
    public int? AantalKamersVan { get; init; }
    public int? AantalKavels { get; init; }
    public string? Adres { get; init; }
    public string? FriendlyUrl { get; init; }
    public string? GewijzigdDatum { get; init; }
    public long? GlobalId { get; init; }
    public string HoofdFoto { get; init; }
    public bool IndIpix { get; init; }
    public bool IndPDF { get; init; }
    public bool IndPlattegrond { get; init; }
    public bool IndTop { get; init; }
    public bool IndVideo { get; init; }
    public string InternalId { get; init; }
    public int? MaxWoonoppervlakte { get; init; }
    public int? MinWoonoppervlakte { get; init; }
    public string? Naam { get; init; }
    public string? Omschrijving { get; init; }
    public IReadOnlyCollection<object> OpenHuizen { get; init; } = new List<object>();
    public string? Plaats { get; init; }
    public object? Prijs { get; init; }
    public string? PrijsGeformatteerd { get; init; }
    public string? PublicatieDatum { get; init; }
    public int Type { get; init; }
    public object? Woningtypen { get; init; }
}
