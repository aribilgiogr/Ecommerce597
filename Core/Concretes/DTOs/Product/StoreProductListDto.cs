namespace Core.Concretes.DTOs.Product
{
    public class StoreProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string SKU { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public bool Active { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
