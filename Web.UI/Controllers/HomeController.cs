using Core.Abstracts.IServices;
using Core.Concretes.DTOs.Mall;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Web.UI.Models;

namespace Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMallStoreFrontService service;

        public HomeController(IMallStoreFrontService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var activeStores = await service.GetActiveStoresAsync();
            var featuredProducts = await service.GetFeaturedProductsAsync();
            var model = new MallHomeDto
            {
                ActiveStores = activeStores,
                FeaturedProducts = featuredProducts
            };
            return View(model);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
