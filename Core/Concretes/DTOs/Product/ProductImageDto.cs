namespace Core.Concretes.DTOs.Product
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsMain { get; set; } = false;
        public int DisplayOrder { get; set; }
    }
}
