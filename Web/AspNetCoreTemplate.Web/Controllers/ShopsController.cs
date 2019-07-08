namespace Olympia.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ShopsController : Controller
    {
        [Authorize]
        public IActionResult Shop()
        {
            return this.View();
        }
    }
}