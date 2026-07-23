using Core.Abstracts.IServices;
using Core.Concretes.DTOs.Sales;
using Core.Concretes.Entities;
using Core.Utils.GenericRepositoryPattern;

namespace Business.Services
{
    public class SalesService : ISalesService
    {
        private readonly IUnitOfWork unitOfWork;

        public SalesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private async Task<Cart> getActiveCartAsync(string memberId)
        {
            var cartRepository = unitOfWork.Repository<Cart>();
            var cart = (await cartRepository.GetManyAsync(x => x.MemeberId == memberId && x.Active)).FirstOrDefault();
            if (cart == null)
            {
                cart = new Cart { MemeberId = memberId };
                await cartRepository.AddAsync(cart);
                await unitOfWork.CommitAsync();
            }
            return cart;
        }

        public async Task<int> AddToCart(string memberId, int productId, int quantity = 1)
        {
            var cartItemRepository = unitOfWork.Repository<CartItem>();
            var cart = await getActiveCartAsync(memberId);
            var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);
            if (item == null)
            {
                item = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity
                };
                await cartItemRepository.AddAsync(item);
            }
            else
            {
                item.Quantity += quantity;
                cartItemRepository.Update(item);
            }
            await unitOfWork.CommitAsync();
            return item.Quantity;
        }

        public async Task<IEnumerable<CartItemListDto>> GetCartItems(string memberId)
        {
            var cart = await getActiveCartAsync(memberId);

            return cart.Items.Select(x => new CartItemListDto
            {
                Id = x.Id,
                Name = x.Product.Name,
                Cover = x.Product.Images.FirstOrDefault(x => x.IsMain)?.ImageUrl,
                Price = x.Product.Price,
                Quantity = x.Quantity
            });
        }

        public async Task<int> RemoveFromCart(string memberId, int productId, int quantity = 1)
        {
            var cartItemRepository = unitOfWork.Repository<CartItem>();

            var cart = await getActiveCartAsync(memberId);

            var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                item.Quantity -= quantity;
                if (item.Quantity <= 0)
                {
                    cartItemRepository.Delete(item);
                }
                else
                {
                    cartItemRepository.Update(item);
                }
                await unitOfWork.CommitAsync();
                return item.Quantity;
            }
            return 0;
        }
    }
}
