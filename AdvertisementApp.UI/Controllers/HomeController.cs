using AdvertisementApp.Application.Interfaces;
using AdvertisementApp.UI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private IProvidedServiceService _providedServiceService;

        public HomeController(IProvidedServiceService providedServiceService)
        {
            _providedServiceService = providedServiceService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _providedServiceService.GetAllAsync();
            return this.ResponseView(response);
        }
        public IActionResult HumanResource()
        {
            return View();
        }
    }
}
