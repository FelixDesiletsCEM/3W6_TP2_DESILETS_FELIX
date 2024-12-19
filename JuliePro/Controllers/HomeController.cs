using JuliePro.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace JuliePro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            var cookie = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture));
            var name = CookieRequestCultureProvider.DefaultCookieName;

            Response.Cookies.Append(name, cookie, new CookieOptions
            {
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddYears(1),
            });

            return LocalRedirect(returnUrl);
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index","Trainer");
        }

     

    }
}