using System.ComponentModel.DataAnnotations;

namespace Core.Concretes.DTOs
{
    public class RegisterAdminDto
    {
        [Display(Name = "İsim", Prompt = "İsim")]
        [Required]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Soyisim", Prompt = "Soyisim")]
        [Required]
        public string LastName { get; set; } = null!;

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
