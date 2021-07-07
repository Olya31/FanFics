using BL.Manager.Interface;
using FanFics.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FanFics.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> Post(TagsViewModel tags, CancellationToken token)
        {
            await _tagsManager.AddTagAsync(tags.ToTag(), token);

            return RedirectToAction("Index", "Profile");
        }
    }
}
