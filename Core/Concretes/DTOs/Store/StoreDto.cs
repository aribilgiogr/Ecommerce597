namespace Core.Concretes.DTOs.Store
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string TaxOffice { get; set; } = null!;
        public string? MersisNumber { get; set; }
        public string ContactPhone { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public decimal CommissionRate { get; set; }
        public bool IsVerified { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
