using Core.Abstracts.IServices;
using Core.Concretes.DTOs.Auth;
using Core.Concretes.Entities;
using Core.Concretes.Enums;
using Microsoft.AspNetCore.Identity;

namespace Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<Member> signInManager;
        private readonly UserManager<Member> userManager;

        public AuthService(SignInManager<Member> signInManager, UserManager<Member> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return new AuthResponseDto { IsSuccessful = true };
            }
            else if (result.IsLockedOut)
            {
                return new AuthResponseDto
                {
                    IsSuccessful = false,
                    Errors = ["Hesabınız çok fazla hatalı deneme nedeniyle askıya alınmıştır!"]
                };
            }
            else if (result.IsNotAllowed)
            {
                return new AuthResponseDto
                {
                    IsSuccessful = false,
                    Errors = ["Giriş yapma izniniz bulunmuyor (Örn: Eposta onayı gerekli olabilir)!"]
                };
            }
            else if (result.RequiresTwoFactor)
            {
                return new AuthResponseDto
                {
                    IsSuccessful = false,
                    Errors = ["İki adımlı doğrulama (2FA) işlemi gereklidir!"]
                };
            }
            else
            {
                return new AuthResponseDto
                {
                    IsSuccessful = false,
                    Errors = ["Geçersiz eposta adresi veya şifre!"]
                };
            }
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<AuthResponseDto> RegisterAdminAsync(RegisterAdminDto model)
        {
            var user = new Member
            {
                UserName = model.Email,
                Email = model.Email,
                MemberType = MemberType.Admin,
                AdminProfile = new Admin
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName
                }
            };
            return await createUserAsync(user, model.Password);
        }

        public async Task<AuthResponseDto> RegisterCustomerAsync(RegisterCustomerDto model)
        {
            var user = new Member
            {
                UserName = model.Email,
                Email = model.Email,
                MemberType = MemberType.Customer,
                CustomerProfile = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName
                }
            };
            return await createUserAsync(user, model.Password);
        }

        public async Task<AuthResponseDto> RegisterStoreAsync(RegisterStoreDto model)
        {
            var user = new Member
            {
                UserName = model.Email,
                Email = model.Email,
                MemberType = MemberType.StoreOwner,
                StoreProfile = new Store
                {
                    StoreName = model.StoreName,
                    TaxNumber = model.TaxNumber,
                    TaxOffice = model.TaxOffice,
                    ContactPhone = model.ContactPhone,
                    ContactEmail = model.ContactEmail,
                }
            };
            return await createUserAsync(user, model.Password);
        }

        private async Task<AuthResponseDto> createUserAsync(Member member, string password)
        {
            var result = await userManager.CreateAsync(member, password);

            if (!result.Succeeded)
            {
                return new AuthResponseDto { IsSuccessful = false, Errors = result.Errors.Select(e => e.Description) };
            }
            else
            {
                return new AuthResponseDto { IsSuccessful = true };
            }
        }
    }
}
