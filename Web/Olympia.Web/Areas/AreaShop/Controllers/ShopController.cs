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

        public ShopController(IShopService shopService)
        {
            this.shopService = shopService;
        }

        public IActionResult ShopIndex()
        {
            var items = this.shopService.GetAllItems();

            ShopViewModel shopViewModel = new ShopViewModel { Items = items };
            return this.View(shopViewModel);
        }

        public async Task<IActionResult> Items(string categoryName)
        {
            var items = await this.shopService.GetAllItemsByCategory(categoryName);
            var shoppingCart = await this.shopService.GetShoppingCartByUserNameAsync(this.User.Identity.Name);

            var shopViewModel = new ShopViewModel() { Items = items, ShoppingCart = shoppingCart };

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
            var result = await
                this.shopService
                .AddItemToUserCart(itemId, this.User.Identity.Name);

            if (!result)
            {
                this.ViewData["Errors"] = GlobalConstants.AlreadyAddedThisItem;
            }

            var cart = await this.shopService.GetShoppingCartByUserNameAsync(this.User.Identity.Name);
            var items = this.shopService.GetAllItems();

            ShopViewModel shopViewModel = new ShopViewModel
            {
                Items = items,
                ShoppingCart = cart
            };

            return this.View("ItemsAll", shopViewModel);
        }

        public async Task<IActionResult> ShoppingCart()
        {
            var cart = await this.shopService.GetShoppingCartDtoByUserNameAsync(this.User.Identity.Name);
            return this.View(cart);
        }

        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            await this.shopService.RemoveFromCartAsync(this.User.Identity.Name, itemId);
            var cart = await this.shopService.GetShoppingCartDtoByUserNameAsync(this.User.Identity.Name);

            return this.View("ShoppingCart", cart);
        }

        public async Task<IActionResult> IncreaseCount(int itemId)
        {
            await this.shopService.IncreaseTimesItemIsBought(itemId);
            var cart = await this.shopService.GetShoppingCartDtoByUserNameAsync(this.User.Identity.Name);

            return this.View("ShoppingCart", cart);
        }

        public async Task<IActionResult> DecreaseCount(int itemId)
        {
            await this.shopService.DecreaseTimesItemIsBought(itemId);
            var cart = await this.shopService.GetShoppingCartDtoByUserNameAsync(this.User.Identity.Name);

            return this.View("ShoppingCart", cart);
        }

        public async Task<IActionResult> FinishOrder()
        {
            var result = await this.shopService.FinishOrderAsync(this.User.Identity.Name);
            this.ViewData["Messages"] = "Your order was successfully created.";

            if (!result)
            {
                this.ViewData["Messages"] = "Your must have at least one item in your cart.";
            }

            var cart = await this.shopService.GetShoppingCartDtoByUserNameAsync(this.User.Identity.Name);
            return this.View("ShoppingCart", cart);
        }

        public async Task<IActionResult> MyOrders()
        {
            var orders = await this.shopService.GetAllOrdersByUsernameAsync(this.User.Identity.Name);

            if (!orders.Any())
            {
                this.ViewData["Errors"] = "You do not have any orders";
            }

            return this.View(orders);
        }

        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            await this.shopService.CompleteOrderAsync(orderId);
            var orders = await this.shopService.GetAllOrdersByUsernameAsync(this.User.Identity.Name);

            if (!orders.Any())
            {
                this.ViewData["Errors"] = "You do not have any orders";
            }

            return this.View("MyOrders", orders);
        }
    }
}