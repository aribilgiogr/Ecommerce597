using Core.Abstracts.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.UI.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly ISalesService salesService;

        public SalesController(ISalesService salesService)
        {
            this.salesService = salesService;
        }

        [HttpPost]
        public async Task<int> AddToCart(int id)
        {
            string? memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (memberId == null) return 0;
            return await salesService.AddToCart(memberId, id);
        }

        [HttpPost]
        public async Task<int> RemoveFromCart(int id)
        {
            string? memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (memberId == null) return 0;
            return await salesService.RemoveFromCart(memberId, id);
        }
    }
}
