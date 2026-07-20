using Core.Concretes.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstracts.IServices
{
    public interface IStoreProductService
    {
        Task<IEnumerable<StoreProductListDto>> GetStoreProductsAsync(int storeId);
        Task<bool> CreateProductAsync(int storeId, CreateProductDto dto);
        Task<bool> UpdateProductAsync(int storeId, UpdateProductDto dto);
        Task<bool> DeleteProductAsync(int storeId, int productId);

        Task<IEnumerable<ProductImageDto>> GetProductImagesAsync(int storeId, int productId);
        Task<bool> AddProductImageAsync(int storeId, int productId, CreateProductImageDto dto);
        Task<bool> DeleteProductImageAsync(int storeId, int productId, int imageId);

        Task<IEnumerable<ProductFeatureDto>> GetProductFeaturesAsync(int storeId, int productId);
        Task<bool> AddProductFeatureAsync(int storeId, int productId, CreateProductFeatureDto dto);
        Task<bool> DeleteProductFeatureAsync(int storeId, int productId, int featureId);
    }
}
