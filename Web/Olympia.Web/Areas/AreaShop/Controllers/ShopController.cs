namespace Olympia.Web.Areas.Shop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
    using Olympia.Services.Contracts;
    using System.Threading.Tasks;

    [Area(GlobalConstants.ShopArea)]
    [Authorize]
    public class ShopController : Controller
    {
        private readonly IShopService shopService;

        public ShopController(
            IShopService shopService)
        {
            this.shopService = shopService;
        }

        public IActionResult ShopIndex()
        {
            var items = this.shopService.GetAllItems();

            return this.View(items);
        }

        public async Task<IActionResult> Items(string categoryName)
        {
            var items = await this.shopService.GetAllItemsByCategory(categoryName);

            return this.View("ItemsAll", items);
        }

        public async Task<IActionResult> ItemDetails(int itemId)
        {
            if (itemId == 0)
            {
                return this.Redirect("/Error");
            }

            var item = await this.shopService.GetItemByIdAsync(itemId);

            return this.View(item);
        }
    }
}