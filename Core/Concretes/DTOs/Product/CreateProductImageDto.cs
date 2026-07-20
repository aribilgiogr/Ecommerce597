namespace Core.Concretes.DTOs.Product
{
    public class CreateProductImageDto
    {
        public string ImageUrl { get; set; } = null!;
        public bool IsMain { get; set; } = false;
        public int DisplayOrder { get; set; }
    }
}
