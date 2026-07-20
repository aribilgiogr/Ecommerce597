namespace Core.Concretes.DTOs.Store
{
    public class UpdateStoreDto
    {
        public string StoreName { get; set; } = null!;
        public string? MersisNumber { get; set; }
        public string ContactPhone { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
    }
}
