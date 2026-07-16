namespace Core.Concretes.DTOs
{
    public class RegisterStoreDto
    {
        public string StoreName { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string TaxOffice { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
