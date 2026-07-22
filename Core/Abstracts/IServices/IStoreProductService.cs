using Core.Concretes.DTOs.Product;

namespace Core.Abstracts.IServices
{
    public interface IStoreProductService
    {
        Task<int> GetCurrentStoreIdAsync(string uid);

        Task<IEnumerable<StoreProductListDto>> GetStoreProductsAsync(int storeId);
        Task<bool> CreateProductAsync(int storeId, CreateProductDto dto);
        Task<UpdateProductDto?> GetProductForUpdateAsync(int storeId, int productId);
        Task<bool> UpdateProductAsync(int storeId, UpdateProductDto dto);
        Task<bool> DeleteProductAsync(int storeId, int productId);

        Task<IEnumerable<ProductImageDto>> GetProductImagesAsync(int storeId, int productId);
        Task<bool> AddProductImageAsync(int storeId, int productId, CreateProductImageDto dto, string uploadRootPath);
        Task<bool> DeleteProductImageAsync(int storeId, int productId, int imageId);
        Task<bool> SetProductMainImageAsync(int storeId, int productId, int imageId);
        Task<bool> UpdateImageDisplayOrderAsync(int storeId, int productId, Dictionary<int, int> imageOrders);


        Task<IEnumerable<ProductFeatureDto>> GetProductFeaturesAsync(int storeId, int productId);
        Task<bool> AddProductFeatureAsync(int storeId, int productId, CreateProductFeatureDto dto);
        Task<bool> DeleteProductFeatureAsync(int storeId, int productId, int featureId);
    }
}
