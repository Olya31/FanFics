using BL.Manager.Interface;
using DAL;
using DAL.Models;
using FanFics.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FanFics.Controllers
{
    public sealed class CompositionController : Controller
    {
        private readonly ILogger<CompositionController> _logger;
        private readonly ICompositionManager _compositionManager;
        private readonly ITagsManager _tagsManager;

        public CompositionController(
            ILogger<CompositionController> logger,
            ICompositionManager compositionManager,
            ITagsManager tagsManager)
        {
            _logger = logger;
            _compositionManager = compositionManager;
            _tagsManager = tagsManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Add");
        }

        [HttpGet]
        public IActionResult Readonly()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult Add(CompositionViewModel composition, CancellationToken token)
        {
            _compositionManager.AddComposition(composition.ToComposition(), token);

            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int? id, CancellationToken token)
        {
            if (id.HasValue)
            {
                await _compositionManager.DeleteCompositionAsync(id.Value, token);

                return RedirectToAction("Index", "Profile");
            }

            return NotFound();
        }

        [HttpPost]
        public JsonResult Search(string Prefix)
        {
            var tagList = _tagsManager.GetTagsSearch(Prefix);

            return Json(tagList);
        }

        //[HttpGet]
        //public ActionResult Edit()
        //{

        //    return RedirectToAction("Index", "Composition");
        //}

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken token)
        {
            if (id.HasValue)
            {
                var compositionViewModels = new CompositionViewModel();

                var composition = await _compositionManager.GetCompositionByIdAsync(id.Value, token);
                var compositionViewModel = compositionViewModels.ToCompositionViewModel(composition);

                return View("edit", compositionViewModel);
            }
            else
            {
                return View("edit");
            }
        }

        [HttpPost]
        public IActionResult Edit(CompositionViewModel composition)
        {
            _compositionManager.Update(composition.ToComposition());

            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public ActionResult ChapterAdd()
        {
            return RedirectToAction("Index", "Chapter");
        }

    }
}
