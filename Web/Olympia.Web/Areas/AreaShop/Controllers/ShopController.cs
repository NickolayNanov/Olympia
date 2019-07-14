namespace Olympia.Web.Areas.Shop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;

    [Area(GlobalConstants.ShopArea)]
    [Authorize]
    public class ShopController : Controller
    {
        public IActionResult ShopIndex()
        {
            return this.View();
        }
    }
}