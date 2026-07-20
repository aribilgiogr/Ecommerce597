using Core.Concretes.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstracts.IServices
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto model);
        Task<AuthResponseDto> RegisterCustomerAsync(RegisterCustomerDto model);
        Task<AuthResponseDto> RegisterAdminAsync(RegisterAdminDto model);
        Task<AuthResponseDto> RegisterStoreAsync(RegisterStoreDto model);
        Task LogoutAsync();
    }
}
