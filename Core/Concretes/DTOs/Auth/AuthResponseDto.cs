namespace Core.Concretes.DTOs.Auth
{
    public class AuthResponseDto
    {
        public bool IsSuccessful { get; set; }
        public string? Token { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
