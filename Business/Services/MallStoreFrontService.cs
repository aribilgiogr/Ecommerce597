using Core.Abstracts.IServices;
using Core.Concretes.DTOs.Mall;
using Core.Concretes.Entities;
using Core.Utils.GenericRepositoryPattern;

namespace Business.Services
{
    public class MallStoreFrontService : IMallStoreFrontService
    {
        private readonly IUnitOfWork unitOfWork;

        public MallStoreFrontService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StoreSummaryDto>> GetActiveStoresAsync()
        {
            var storeRepository = unitOfWork.Repository<Store>();
            var stores = await storeRepository.GetManyAsync(s => s.IsVerified);

            return stores.Select(s => new StoreSummaryDto { Id = s.Id, StoreName = s.StoreName }).ToList();
        }

        public async Task<IEnumerable<ProductSummaryDto>> GetFeaturedProductsAsync()
        {
            var productRepository = unitOfWork.Repository<Product>();
            var products = await productRepository.GetManyAsync(p => p.Active && !p.Deleted);

            return products.OrderByDescending(p => p.UpdatedAt ?? p.CreatedAt)
                           .Take(20)
                           .Select(p => new ProductSummaryDto
                           {
                               Id = p.Id,
                               StoreId = p.StoreId,
                               StoreName = p.Store.StoreName,
                               Name = p.Name,
                               Price = p.Price,
                               MainImageUrl = p.Images?.FirstOrDefault(i => i.IsMain)?.ImageUrl ?? null
                           })
                           .ToList();
        }
    }
}
