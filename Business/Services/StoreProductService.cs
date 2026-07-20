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

            productRepository.Delete(product);
            int affectedRows = await unitOfWork.CommitAsync();

            return affectedRows > 0;
        }

        public Task<bool> DeleteProductFeatureAsync(int storeId, int productId, int featureId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductImageAsync(int storeId, int productId, int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductFeatureDto>> GetProductFeaturesAsync(int storeId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductImageDto>> GetProductImagesAsync(int storeId, int productId)
        {
            throw new NotImplementedException();
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
                CategoryName = p.Category.Name,
                BrandName = p.Brand.Name,
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
