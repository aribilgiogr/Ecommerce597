namespace Core.Concretes.DTOs.Product
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string SKU { get; set; } = null!;
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public bool Active { get; set; }
    }
}
