using Core.Abstracts.IServices;
using Core.Concretes.DTOs.Product;
using Core.Concretes.Entities;
using Core.Utils.GenericRepositoryPattern;

namespace Business.Services
{
    public class StoreProductService : IStoreProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public StoreProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> AddProductFeatureAsync(int storeId, int productId, CreateProductFeatureDto dto)
        {
            var productRepository = unitOfWork.Repository<Product>();
            var product = await productRepository.GetByIdAsync(productId);

            if (product == null || product.StoreId != storeId) return false;

            var featureRepository = unitOfWork.Repository<ProductFeature>();
            var feature = new ProductFeature
            {
                ProductId = productId,
                FeatureName = dto.FeatureName,
                FeatureValue = dto.FeatureValue,
                DisplayOrder = dto.DisplayOrder,
            };
            await featureRepository.AddAsync(feature);
            int affectedRows = await unitOfWork.CommitAsync();

            return affectedRows > 0;
        }

        public async Task<bool> AddProductImageAsync(int storeId, int productId, CreateProductImageDto dto)
        {
            var productRepository = unitOfWork.Repository<Product>();
            var product = await productRepository.GetByIdAsync(productId);

            if (product == null || product.StoreId != storeId) return false;

            var imageRepository = unitOfWork.Repository<ProductImage>();
            var image = new ProductImage
            {
                ProductId = productId,
                ImageUrl = dto.ImageUrl,
                DisplayOrder = dto.DisplayOrder,
                IsMain = dto.IsMain,
            };

            await imageRepository.AddAsync(image);
            int affectedRows = await unitOfWork.CommitAsync();

            return affectedRows > 0;
        }

        public async Task<bool> CreateProductAsync(int storeId, CreateProductDto dto)
        {
            var product = new Product
            {
                StoreId = storeId,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                SKU = dto.SKU,
                BrandId = dto.BrandId,
                CategoryId = dto.CategoryId
            };
            var productRepository = unitOfWork.Repository<Product>();

            await productRepository.AddAsync(product);
            int affectedRows = await unitOfWork.CommitAsync();

            return affectedRows > 0;
        }

        public async Task<bool> DeleteProductAsync(int storeId, int productId)
        {
            var productRepository = unitOfWork.Repository<Product>();
            var product = await productRepository.GetByIdAsync(productId);
            if (product == null || product.StoreId != storeId) return false;

            // Hard/Permenant Delete: Kalıcı silme işlemi.
            // productRepository.Delete(product);

            // Soft Delete: Silinmiş gösterme işlemi, Deleted özelliğini 'true' yaparız.
            product.Deleted = true;
            product.Active = false;
            productRepository.Update(product);


            int affectedRows = await unitOfWork.CommitAsync();

            return affectedRows > 0;
        }

        public async Task<bool> DeleteProductFeatureAsync(int storeId, int productId, int featureId)
        {
            var productRepository = unitOfWork.Repository<Product>();
            var product = await productRepository.GetByIdAsync(productId);

            if (product == null || product.StoreId != storeId) return false;

            var featureRepository = unitOfWork.Repository<ProductFeature>();
            var feature = await featureRepository.GetByIdAsync(featureId);

            if (feature == null || feature.ProductId != productId) return false;

            featureRepository.Delete(feature);
            int affectedRows = await unitOfWork.CommitAsync();

            return affectedRows > 0;
        }

        public async Task<bool> DeleteProductImageAsync(int storeId, int productId, int imageId)
        {
            var productRepository = unitOfWork.Repository<Product>();
            var product = await productRepository.GetByIdAsync(productId);

            if (product == null || product.StoreId != storeId) return false;

            var imageRepository = unitOfWork.Repository<ProductImage>();
            var image = await imageRepository.GetByIdAsync(imageId);

            if (image == null || image.ProductId != productId) return false;

            imageRepository.Delete(image);
            int affectedRows = await unitOfWork.CommitAsync();

            return affectedRows > 0;
        }

        public async Task<int> GetCurrentStoreIdAsync(string uid)
        {
            var storeRepository = unitOfWork.Repository<Store>();
            var stores = await storeRepository.GetManyAsync(s => s.MemberId == uid);
            return stores.FirstOrDefault()?.Id ?? 0;
        }

        public async Task<IEnumerable<ProductFeatureDto>> GetProductFeaturesAsync(int storeId, int productId)
        {
            var productRepository = unitOfWork.Repository<Product>();
            var product = await productRepository.GetByIdAsync(productId);

            if (product == null || product.StoreId != storeId) return [];

            var featureRepository = unitOfWork.Repository<ProductFeature>();
            var features = await featureRepository.GetManyAsync(f => f.ProductId == productId);

            return features.OrderBy(f => f.DisplayOrder)
                           .Select(f => new ProductFeatureDto
                           {
                               Id = f.Id,
                               DisplayOrder = f.DisplayOrder,
                               FeatureName = f.FeatureName,
                               FeatureValue = f.FeatureValue,
                           });
        }

        public async Task<UpdateProductDto?> GetProductForUpdateAsync(int storeId, int productId)
        {
            var productRepository = unitOfWork.Repository<Product>();
            var product = await productRepository.GetByIdAsync(productId);

            if (product == null || product.StoreId != storeId) return null;

            return new UpdateProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                SKU = product.SKU,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                Active = product.Active
            };
        }

        public async Task<IEnumerable<ProductImageDto>> GetProductImagesAsync(int storeId, int productId)
        {
            var productRepository = unitOfWork.Repository<Product>();
            var product = await productRepository.GetByIdAsync(productId);

            if (product == null || product.StoreId != storeId) return [];

            var imageRepository = unitOfWork.Repository<ProductImage>();
            var images = await imageRepository.GetManyAsync(i => i.ProductId == productId);

            return images.OrderBy(i => i.DisplayOrder)
                         .Select(i => new ProductImageDto
                         {
                             Id = i.Id,
                             DisplayOrder = i.DisplayOrder,
                             ImageUrl = i.ImageUrl,
                             IsMain = i.IsMain
                         });
        }

        public async Task<IEnumerable<StoreProductListDto>> GetStoreProductsAsync(int storeId)
        {
            var productRepository = unitOfWork.Repository<Product>();
            var products = await productRepository.GetManyAsync(p => p.StoreId == storeId);

            return products.Select(p => new StoreProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                SKU = p.SKU,
                CategoryName = p.Category?.Name ?? string.Empty,
                BrandName = p.Brand?.Name ?? string.Empty,
                Active = p.Active,
                UpdatedAt = p.UpdatedAt ?? p.CreatedAt
            });
        }

        public async Task<bool> UpdateProductAsync(int storeId, UpdateProductDto dto)
        {
            var productRepository = unitOfWork.Repository<Product>();
            var product = await productRepository.GetByIdAsync(dto.Id);
            if (product == null || product.StoreId != storeId) return false;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.SKU = dto.SKU;
            product.BrandId = dto.BrandId;
            product.CategoryId = dto.CategoryId;
            product.Active = dto.Active;

            productRepository.Update(product);
            int affectedRows = await unitOfWork.CommitAsync();

            return affectedRows > 0;
        }
    }
}
