namespace Core.Concretes.DTOs.Sales
{
    public class CartItemListDto
    {
        public int Id { get; set; }
        public string? Cover { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
}
