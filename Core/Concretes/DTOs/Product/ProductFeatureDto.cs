namespace Core.Concretes.DTOs.Product
{
    public class ProductFeatureDto
    {
        public int Id { get; set; }
        public string FeatureName { get; set; } = null!;
        public string FeatureValue { get; set; } = null!;
        public int DisplayOrder { get; set; }
    }
}
