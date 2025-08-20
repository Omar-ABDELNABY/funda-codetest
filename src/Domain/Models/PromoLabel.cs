namespace Domain.Models;

public record PromoLabel
{
    public bool HasPromotionLabel { get; init; }
    public IReadOnlyCollection<string> PromotionPhotos { get; init; } = new List<string>();
    public IReadOnlyCollection<string>? PromotionPhotosSecure { get; init; }
    public int PromotionType { get; init; }
    public int RibbonColor { get; init; }
    public string? RibbonText { get; init; }
    public string? Tagline { get; init; }
}
