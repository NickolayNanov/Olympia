namespace Olympia.Web.Areas.Shop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
    using Olympia.Data.Models.ViewModels.Shop;
    using Olympia.Services.Contracts;
    using System.Linq;
    using System.Threading.Tasks;

    [Area(GlobalConstants.ShopArea)]
    [Authorize]
    public class ShopController : Controller
    {
        private readonly IShopService shopService;
        private readonly IUsersService usersService;

        public ShopController(
            IShopService shopService,
            IUsersService usersService)
        {
            this.shopService = shopService;
            this.usersService = usersService;
        }

        public IActionResult ShopIndex()
        {
            var items = this.shopService.GetAllItems();

            ShopViewModel shopViewModel = new ShopViewModel { Items = items};
            return this.View(shopViewModel);
        }

        public async Task<IActionResult> Items(string categoryName)
        {
            var items = await this.shopService.GetAllItemsByCategory(categoryName);
            var shopViewModel = new ShopViewModel() { Items = items };

            return this.View("ItemsAll", shopViewModel);
        }

        public async Task<IActionResult> ItemDetails(int itemId)
        {
            if (itemId == 0)
            {
                return this.Redirect("/Error");
            }

            var item = await this.shopService.GetItemDtoByIdAsync(itemId);

            return this.View(item);
        }

        public async Task<IActionResult> AddToCart(int itemId)
        {
            var item = await this.shopService.GetItemDtoByIdAsync(itemId);
            var user = await this.usersService.GetUserByUsernameAsync(this.User.Identity.Name);

            await this.shopService.AddItemToUserCart(itemId, user);

            return this.View("ShoppingCart", user.ShoppingCart);
        }
    }
}