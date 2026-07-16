using System.ComponentModel.DataAnnotations;

namespace Core.Concretes.DTOs
{
    public class RegisterStoreDto
    {
        [Required]
        [Display(Name = "Mağaza Adı", Prompt = "Mağaza Adı")]
        public string StoreName { get; set; } = null!;

        [Required]
        [Display(Name = "Vergi No", Prompt = "Vergi No")]
        public string TaxNumber { get; set; } = null!;

        [Required]
        [Display(Name = "Vergi Dairesi", Prompt = "Vergi Dairesi")]
        public string TaxOffice { get; set; } = null!;

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "İletişim Numarası", Prompt = "İletişim Numarası")]
        public string ContactPhone { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "İletişim Eposta Adresi", Prompt = "İletişim Eposta Adresi")]
        public string ContactEmail { get; set; } = null!;

        [EmailAddress]
        [Display(Name = "Eposta Adresi", Prompt = "Eposta Adresi")]
        [Required]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Parola", Prompt = "Parola")]
        [Required]
        public string Password { get; set; } = null!;
    }
}
