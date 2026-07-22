using Core.Concretes.DTOs.Mall;

namespace Core.Abstracts.IServices
{
    public interface IMallStoreFrontService
    {
        Task<IEnumerable<StoreSummaryDto>> GetActiveStoresAsync();
        Task<IEnumerable<ProductSummaryDto>> GetFeaturedProductsAsync();
    }
}
