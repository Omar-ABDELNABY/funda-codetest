namespace Domain.Models;

public record Prijs
{
    public bool GeenExtraKosten { get; init; }
    public string HuurAbbreviation { get; init; }
    public int? Huurprijs { get; init; }
    public string HuurprijsOpAanvraag { get; init; }
    public int? HuurprijsTot { get; init; }
    public string KoopAbbreviation { get; init; }
    public int? Koopprijs { get; init; }
    public string KoopprijsOpAanvraag { get; init; }
    public int? KoopprijsTot { get; init; }
    public int? OriginelePrijs { get; init; }
    public string VeilingText { get; init; }
}
