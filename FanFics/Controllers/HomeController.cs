using BL.Manager.Interface;
using FanFics.Models;
using FanFics.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FanFics.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChapterManager _chapterManager;

        public HomeController(ILogger<HomeController> logger, IChapterManager chapterManager)
        {
            _logger = logger;
            _chapterManager = chapterManager;
        }

        public IActionResult Index()
        {
            //var chapterViewModels = new ChapterViewModel();

            //var chapters =  _chapterManager.GetChapters();
            //var chapterViewModel = chapterViewModels.ToChapterViewModels(chapters);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
