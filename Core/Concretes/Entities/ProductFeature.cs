using Core.Abstracts.Bases;

namespace Core.Concretes.Entities
{
    public class ProductFeature : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public string FeatureName { get; set; } = null!;
        public string FeatureValue { get; set; } = null!;
        public int DisplayOrder { get; set; }
    }
}
