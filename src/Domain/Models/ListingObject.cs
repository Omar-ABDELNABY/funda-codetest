using System.Text.Json.Serialization;

namespace Domain.Models;

public record ListingObject
{
    public string AangebodenSindsTekst { get; init; }
    public string AanmeldDatum { get; init; }
    public int? AantalBeschikbaar { get; init; }
    public int? AantalKamers { get; init; }
    public int? AantalKavels { get; init; }
    public string Aanvaarding { get; init; }
    public string Adres { get; init; }
    public double Afstand { get; init; }
    public string BronCode { get; init; }
    public IReadOnlyCollection<object> ChildrenObjects { get; init; } = new List<object>();
    public string? DatumAanvaarding { get; init; }
    public string? DatumOndertekeningAkte { get; init; }
    public string Foto { get; init; }
    public string FotoLarge { get; init; }
    public string FotoLargest { get; init; }
    public string FotoMedium { get; init; }
    public string FotoSecure { get; init; }
    public string? GewijzigdDatum { get; init; }
    public long GlobalId { get; init; }
    public string GroupByObjectType { get; init; }
    public bool Heeft360GradenFoto { get; init; }
    public bool HeeftBrochure { get; init; }
    public bool HeeftOpenhuizenTopper { get; init; }
    public bool HeeftOverbruggingsgrarantie { get; init; }
    public bool HeeftPlattegrond { get; init; }
    public bool HeeftTophuis { get; init; }
    public bool HeeftVeiling { get; init; }
    public bool HeeftVideo { get; init; }
    public int? HuurPrijsTot { get; init; }
    public int? Huurprijs { get; init; }
    public string? HuurprijsFormaat { get; init; }
    public string Id { get; init; }
    public int? InUnitsVanaf { get; init; }
    public bool IndProjectObjectType { get; init; }
    public bool? IndTransactieMakelaarTonen { get; init; }
    public bool IsSearchable { get; init; }
    public bool IsVerhuurd { get; init; }
    public bool IsVerkocht { get; init; }
    public bool IsVerkochtOfVerhuurd { get; init; }
    public int? Koopprijs { get; init; }
    public string KoopprijsFormaat { get; init; }
    public int? KoopprijsTot { get; init; }
    public string? Land { get; init; }
    public int MakelaarId { get; init; }
    public string MakelaarNaam { get; init; }
    public string MobileURL { get; init; }
    public string? Note { get; init; }
    public IReadOnlyCollection<object> OpenHuis { get; init; } = new List<object>();
    public int? Oppervlakte { get; init; }
    public int? Perceeloppervlakte { get; init; }
    public string Postcode { get; init; }
    public Prijs Prijs { get; init; }
    public string PrijsGeformatteerdHtml { get; init; }
    public string PrijsGeformatteerdTextHuur { get; init; }
    public string PrijsGeformatteerdTextKoop { get; init; }
    public IReadOnlyCollection<string> Producten { get; init; } = new List<string>();
    public Project Project { get; init; }
    public string? ProjectNaam { get; init; }
    public PromoLabel PromoLabel { get; init; }
    public string PublicatieDatum { get; init; }
    public int? PublicatieStatus { get; init; }
    public string? SavedDate { get; init; }
    [JsonPropertyName("Soort-aanbod")]
    public string SoortAanbodName { get; init; }
    public int? SoortAanbod { get; init; }
    public string? StartOplevering { get; init; }
    public string? TimeAgoText { get; init; }
    public string? TransactieAfmeldDatum { get; init; }
    public int? TransactieMakelaarId { get; init; }
    public string? TransactieMakelaarNaam { get; init; }
    public int? TypeProject { get; init; }
    public string URL { get; init; }
    public string VerkoopStatus { get; init; }
    public double WGS84_X { get; init; }
    public double WGS84_Y { get; init; }
    public int? WoonOppervlakteTot { get; init; }
    public int? Woonoppervlakte { get; init; }
    public string Woonplaats { get; init; }
    public IReadOnlyCollection<int> ZoekType { get; init; } = new List<int>();
}
