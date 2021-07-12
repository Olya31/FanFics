using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FanFics.Controllers
{
    [Authorize]
    public sealed class SettingsController : Controller
    {
        public IActionResult Settings()
        {
            return View("Settings");
        }

        [HttpPost]
        public IActionResult SetThemeToCookie(string themeUrl)
        {
            HttpContext.Response.Cookies.Append("theme", themeUrl, 
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) } );
            return Ok();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }   
    }
}
