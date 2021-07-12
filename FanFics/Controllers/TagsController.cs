using BL.Manager.Interface;
using FanFics.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FanFics.Controllers
{
    [Authorize]
    public sealed class TagsController : Controller
    {
        private readonly ILogger<TagsController> _logger;
        private readonly ITagsManager _tagsManager;

        public TagsController(
            ILogger<TagsController> logger,
            ITagsManager tagsManager)
        {
            _logger = logger;
            _tagsManager = tagsManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("AddTags");
        }

        [HttpPost]
        public async Task<ActionResult> AddTag(TagsViewModel tag, CancellationToken token)
        {
            await _tagsManager.AddTagAsync(tag.ToTag(), token);
            return RedirectToAction("Index", "Profile");
        }
    }
}
