using BL.Manager.Interface;
using DAL.Models;
using FanFics.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FanFics.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly ILogger<FavoriteController> _logger;
        private readonly ICompositionManager _compositionManager;
        private readonly IFavoriteManager _favoriteManager;
        private readonly UserManager<User> _userManager;

        public FavoriteController(
            ILogger<FavoriteController> logger,
            ICompositionManager compositionManager,
            IFavoriteManager favoriteManager,
            UserManager<User> userManager)
        {
            _logger = logger;
            _compositionManager = compositionManager;
            _favoriteManager = favoriteManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken token)
        {
            var user = await GetCurrentUserAsync();
            var favorites = await _favoriteManager.GetFavoriteByUserIdAsync(user.Id, token);

            var model = new FavoriteViewModel().ToFavoriteViewModel(favorites);
            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int? id, CancellationToken token)
        {
            if (id.HasValue)
            {
                await _favoriteManager.DeleteFavoriteAsync(id.Value, token);

                return RedirectToAction("Index");
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> AddFavorite(int compositionId, string userId, CancellationToken token)
        {
            var favorite = new Favorite
            {
                CompositionId = compositionId,
                UserId = userId,
            };

            await _favoriteManager.AddFavoriteAsync(favorite, token);

            return RedirectToAction("Index");
        }

        private async Task<User> GetCurrentUserAsync()
        {
            var currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _userManager.FindByIdAsync(currentUserName);
        }
    }
}
