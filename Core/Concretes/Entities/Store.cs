using Core.Abstracts.Bases;

namespace Core.Concretes.Entities
{
    public class Store : BaseEntity
    {
        public string MemberId { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;

        public string StoreName { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string TaxOffice { get; set; } = null!;
        public string? MersisNumber { get; set; }
        public string ContactPhone { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public decimal CommissionRate { get; set; }
        public bool IsVerified { get; set; } = false;

        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
