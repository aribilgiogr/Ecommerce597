using Core.Abstracts.IServices;
using Core.Concretes.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.UI.Controllers
{
    [Authorize]
    public class StoreProductController : Controller
    {
        private readonly IStoreProductService storeProductService;
        private readonly UserManager<Member> userManager;

        public StoreProductController(IStoreProductService storeProductService, UserManager<Member> userManager)
        {
            this.userManager = userManager;
            this.storeProductService = storeProductService;
        }

        private async Task<int> GetCurrentStoreIdAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return 0;

            return await storeProductService.GetCurrentStoreIdAsync(user.Id);
        }


        public async Task<IActionResult> Index()
        {
            var storeId = await GetCurrentStoreIdAsync();
            if (storeId == 0) return Forbid();

            return View(await storeProductService.GetStoreProductsAsync(storeId));
        }
    }
}
