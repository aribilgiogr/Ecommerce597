using System.ComponentModel.DataAnnotations;

namespace Core.Concretes.DTOs.Auth
{
    public class LoginDto
    {
        [EmailAddress]
        [Display(Name = "Eposta Adresi", Prompt = "Eposta Adresi")]
        [Required]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Parola", Prompt = "Parola")]
        [Required]
        public string Password { get; set; } = null!;

        [Display(Name = "Beni Hatırla", Prompt = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
