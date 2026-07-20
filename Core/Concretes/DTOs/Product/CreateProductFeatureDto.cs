namespace Core.Concretes.DTOs.Product
{
    public class CreateProductFeatureDto
    {
        public string FeatureName { get; set; } = null!;
        public string FeatureValue { get; set; } = null!;
        public int DisplayOrder { get; set; }
    }
}
