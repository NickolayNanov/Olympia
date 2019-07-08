namespace Olympia.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Services.Data;
    using Olympia.Web.Areas.Administration.ViewModels.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
