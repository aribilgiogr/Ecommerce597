using Core.Concretes.DTOs.Sales;

namespace Core.Abstracts.IServices
{
    public interface ISalesService
    {
        Task<int> AddToCart(string memberId, int productId, int quantity = 1);
        Task<int> RemoveFromCart(string memberId, int productId, int quantity = 1);
        Task<IEnumerable<CartItemListDto>> GetCartItems(string memberId);
    }
}
