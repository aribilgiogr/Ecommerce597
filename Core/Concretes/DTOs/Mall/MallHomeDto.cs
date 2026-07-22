namespace Core.Concretes.DTOs.Mall
{
    public class MallHomeDto
    {
        public IEnumerable<StoreSummaryDto> ActiveStores { get; set; } = [];
        public IEnumerable<ProductSummaryDto> FeaturedProducts { get; set; } = [];

    }
}
