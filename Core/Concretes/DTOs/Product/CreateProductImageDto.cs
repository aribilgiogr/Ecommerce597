using Microsoft.AspNetCore.Http;

namespace Core.Concretes.DTOs.Product
{
    public class CreateProductImageDto
    {
        public IFormFile? File { get; set; }
        public bool IsMain { get; set; } = false;
    }
}
