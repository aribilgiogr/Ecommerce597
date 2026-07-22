using Core.Abstracts.IServices;
using Core.Concretes.DTOs.Product;
using Core.Concretes.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Web.UI.Controllers
{
    [Authorize]
    public class StoreProductController : Controller
    {
        private readonly IStoreProductService storeProductService;
        private readonly UserManager<Member> userManager;
        private readonly ICategoryService categoryService;
        private readonly IBrandService brandService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public StoreProductController(IStoreProductService storeProductService, UserManager<Member> userManager, ICategoryService categoryService, IBrandService brandService, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.storeProductService = storeProductService;
            this.categoryService = categoryService;
            this.brandService = brandService;
            this.webHostEnvironment = webHostEnvironment;
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

        public async Task<IActionResult> Create()
        {
            var storeId = await GetCurrentStoreIdAsync();
            if (storeId == 0) return Forbid();

            ViewBag.Categories = new SelectList(await categoryService.GetCategoriesForSelectionAsync(), "Id", "Name");
            ViewBag.Brands = new SelectList(await brandService.GetBrandsForSelectionAsync(), "Id", "Name");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto model)
        {
            if (ModelState.IsValid)
            {
                var storeId = await GetCurrentStoreIdAsync();
                if (storeId == 0) return Forbid();

                if (await storeProductService.CreateProductAsync(storeId, model))
                    return RedirectToAction("index");

                ModelState.AddModelError(string.Empty, "Ürün kayıt işlemi sırasında bir hata oluştu.");
            }
            ViewBag.Categories = new SelectList(await categoryService.GetCategoriesForSelectionAsync(), "Id", "Name", model.CategoryId);
            ViewBag.Brands = new SelectList(await brandService.GetBrandsForSelectionAsync(), "Id", "Name", model.BrandId);
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var storeId = await GetCurrentStoreIdAsync();
            if (storeId == 0) return Forbid();

            var model = await storeProductService.GetProductForUpdateAsync(storeId, id);
            if (model == null) return NotFound();

            ViewBag.Categories = new SelectList(await categoryService.GetCategoriesForSelectionAsync(), "Id", "Name", model.CategoryId);

            ViewBag.Brands = new SelectList(await brandService.GetBrandsForSelectionAsync(), "Id", "Name", model.BrandId);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductDto model)
        {
            if (ModelState.IsValid)
            {
                var storeId = await GetCurrentStoreIdAsync();
                if (storeId == 0) return Forbid();

                if (await storeProductService.UpdateProductAsync(storeId, model))
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Categories = new SelectList(await categoryService.GetCategoriesForSelectionAsync(), "Id", "Name", model.CategoryId);
            ViewBag.Brands = new SelectList(await brandService.GetBrandsForSelectionAsync(), "Id", "Name", model.BrandId);
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var storeId = await GetCurrentStoreIdAsync();
            if (storeId == 0) return Forbid();

            var model = await storeProductService.GetProductForUpdateAsync(storeId, id);
            if (model == null) return NotFound();

            ViewBag.Categories = new SelectList(await categoryService.GetCategoriesForSelectionAsync(), "Id", "Name", model.CategoryId);
            ViewBag.Brands = new SelectList(await brandService.GetBrandsForSelectionAsync(), "Id", "Name", model.BrandId);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storeId = await GetCurrentStoreIdAsync();
            if (storeId == 0) return Forbid();

            if (await storeProductService.DeleteProductAsync(storeId, id))
            {
                return RedirectToAction("index");
            }

            // Farklı bir action yapısına bilgi göndermek için TempData kullanılır.
            TempData["ErrorMessage"] = "Ürün silinirken bir hata oluştu.";
            return RedirectToAction("delete", new { id });
        }

        public async Task<IActionResult> Images(int id)
        {
            var storeId = await GetCurrentStoreIdAsync();
            if (storeId == 0) return Forbid();

            var images = await storeProductService.GetProductImagesAsync(storeId, id);
            ViewBag.Images = images;
            ViewBag.ProductId = id;

            return View();
        }
    }
}
