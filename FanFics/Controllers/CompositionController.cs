using BL.Manager.Interface;
using DAL.Models;
using FanFics.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FanFics.Controllers
{

    public sealed class CompositionController : Controller
    {
        private readonly ILogger<CompositionController> _logger;
        private readonly ICompositionManager _compositionManager;
        private readonly ITagsManager _tagsManager;
        private readonly IRatingManager _ratingManager;
        private readonly UserManager<User> _userManager;

        public CompositionController(
            ILogger<CompositionController> logger,
            ICompositionManager compositionManager,
            ITagsManager tagsManager,
            UserManager<User> userManager,
            IRatingManager ratingManager)
        {
            _logger = logger;
            _compositionManager = compositionManager;
            _tagsManager = tagsManager;
            _userManager = userManager;
            _ratingManager = ratingManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            return View("Add", user);
        }

        [HttpPost]
        public IActionResult Add(CompositionViewModel composition, User user, CancellationToken token)
        {
            _compositionManager.AddComposition(composition.ToComposition(user.Id), token);

            return RedirectToAction("Index", "Profile", new { user = user });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int? id, CancellationToken token)
        {
            if (id.HasValue)
            {
                await _compositionManager.DeleteCompositionAsync(id.Value, token);

                return RedirectToAction("Index", "Profile", new { id = id });
            }

            return NotFound();
        }

        [HttpPost]
        public JsonResult Search(string Prefix)
        {
            var tagList = _tagsManager.GetTagsSearch(Prefix);

            return Json(tagList);
        }

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
                return View("Index", "Profile");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompositionViewModel composition, CancellationToken token)
        {
            await _compositionManager.EditCompositionAsync(composition.ToComposition(composition), token);

            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public async Task<ActionResult> ChapterAdd(int? id, CancellationToken token)
        {
            var composition = await _compositionManager.GetCompositionByIdAsync(id.Value, token);

            return RedirectToAction("Index", "Chapter", composition);
        }

        [HttpGet]
        public async Task<ActionResult> Readonly(int? id, CancellationToken token)
        {
            var compositionViewModel = await GetCompositionViewModelAsync(id.Value, token);

            var rating = new RatingViewModel();
            rating.CompositionId = id.Value;

            var Comments = _ratingManager.GetComments(id);
            rating.ListOfComments = Comments;

            var ratings = _ratingManager.GetRating(id);

            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.RatingCounter);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            var user = await GetCurrentUserAsync();

            var item = GetItemByRead(compositionViewModel, rating, user);

            return View("Readonly", item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddComment(ReadonlyViewModel ratings, CancellationToken token)
        {
            var comment = ratings.Rating.Comments;
            var compositionId = ratings.Rating.CompositionId;
            var rating = ratings.Rating.RatingCounter;
            var userId = ratings.User.Id;

            await _ratingManager.AddRating(ratings.Rating.ToRating(comment, compositionId, rating, userId), token);

            return RedirectToAction("Readonly", new { id = compositionId });
        }

        private async Task<User> GetCurrentUserAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = this.User;
                var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                return await _userManager.FindByIdAsync(currentUserName);
            }
            return null;
        }

        public async Task<CompositionViewModel> GetCompositionViewModelAsync(int? id, CancellationToken token)
        {
            var compositionViewModels = new CompositionViewModel();
            var composition = await _compositionManager.GetCompositionByIdAsync(id.Value, token);
            return compositionViewModels.ToCompositionViewModel(composition);
        }

        public ReadonlyViewModel GetItemByRead(
            CompositionViewModel compositionViewModel,
            RatingViewModel rating,
            User user)
        {
            return new ReadonlyViewModel
            {
                Composition = compositionViewModel,
                Rating = rating,
                User = user
            };
        }
    }
}
