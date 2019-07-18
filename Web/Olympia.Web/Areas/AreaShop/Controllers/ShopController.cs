namespace Olympia.Web.Areas.Shop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
    using Olympia.Services.Contracts;

    [Area(GlobalConstants.ShopArea)]
    [Authorize]
    public class ShopController : Controller
    {
        private readonly IShopService shopService;

        public ShopController(IShopService shopService)
        {
            this.shopService = shopService;
        }

        public IActionResult ShopIndex()
        {
            var items = this.shopService.GetAllItems();

            return this.View(items);
        }
    }
}