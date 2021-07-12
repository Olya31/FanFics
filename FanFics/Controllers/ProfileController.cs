using BL.Manager.Interface;
using DAL.Models;
using FanFics.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FanFics.Controllers
{
    [Authorize]
    public sealed class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ITagsManager _tagsManager;
        private readonly ICompositionManager _compositionManager;

        public ProfileController(
            UserManager<User> userManager,
            ITagsManager tagsManager,
            ICompositionManager compositionManager)
        {
            _userManager = userManager;
            _tagsManager = tagsManager;
            _compositionManager = compositionManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            string titleComposition, 
            string sortOrder, 
            User user, 
            CancellationToken token)
        {
            var userIdentity = await GetCurrentUserAsync();

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.FandomSortParm = sortOrder == "Fandom" ? "fandom_desc" : "Fandom";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var sortComposition = _compositionManager.Sort(sortOrder, userIdentity.Id);
            var filterComposition = await _compositionManager.FilterAsync(titleComposition, userIdentity.Id, token);

            var items = await GetItemInView(titleComposition, sortOrder, sortComposition, filterComposition, user, token);

            return View("Index", items);
        }

        public async Task<ProfileViewModel> GetItemInView(
            string titleComposition, 
            string sortOrder, 
            List<Composition> sortComposition,
            IList<Composition> filterComposition,
            User user,
            CancellationToken token)
        {
            var userIdentity = await GetCurrentUserAsync();
            var compositionViewModel = new CompositionViewModel();
            var compositionsList = await _compositionManager.GetCompositionsByUser(userIdentity.Id, token);

            var item = new ProfileViewModel();

            if (user.Email != null)
            {
                item.User = user;
            }
            else
            {
                item.User = await GetCurrentUserAsync();
            }                      
            if (titleComposition == null && sortOrder == null)
            {
                item.Compositions = compositionViewModel.ToCompositionViewModels(compositionsList);
            }
            if (sortOrder != null)
            {
                item.Compositions = compositionViewModel.ToCompositionViewModels(sortComposition);
            }
            if (titleComposition != null)
            {
                item.Compositions = compositionViewModel.ToCompositionViewModels(filterComposition);
            }

            return item;
        }

        [HttpPost]
        public async Task<IActionResult> EditNickName(string value)
        {
            var user = await GetCurrentUserAsync();

            user.NickName = value;
            await _userManager.UpdateAsync(user);

            return Ok(true);
        }

        [HttpPost]
        public async Task<IActionResult> EditFullName(string value)
        {
            var user = await GetCurrentUserAsync();

            user.FullName = value;
            await _userManager.UpdateAsync(user);

            return Ok(true);
        }

        [HttpPost]
        public async Task<IActionResult> EditBirthday(DateTime value)
        {
            var user = await GetCurrentUserAsync();

            user.BirthDay = value;
            await _userManager.UpdateAsync(user);

            return Ok(true);
        }

        private async Task<User> GetCurrentUserAsync()
        {
            var currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _userManager.FindByIdAsync(currentUserName);
        }
    }
}
