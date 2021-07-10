using BL.Manager.Interface;
using DAL.Models;
using FanFics.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FanFics.Controllers
{
    public sealed class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICompositionManager _compositionManager;

        public ProfileController(
            UserManager<User> userManager,
            ICompositionManager compositionManager)
        {
            _userManager = userManager;
            _compositionManager = compositionManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string titleComposition, string sortOrder, User user)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.FandomSortParm = sortOrder == "Fandom" ? "fandom_desc" : "Fandom";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var sortComposition = _compositionManager.Sort(sortOrder);
            var filterComposition = _compositionManager.Filter(titleComposition);

            var items = await GetItemInView(titleComposition, sortOrder, sortComposition, filterComposition, user);

            return View("Index", items);
        }

        public async Task<ProfileViewModel> GetItemInView(
            string titleComposition, 
            string sortOrder, 
            List<Composition> sortComposition,
            List<Composition> filterComposition,
            User user)
        {
            var compositionViewModel = new CompositionViewModel();
            var compositionsList = _compositionManager.GetCompositions();

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

        [HttpGet]
        public ActionResult Edit()
        {
            return RedirectToAction("Index", "Composition");
        }

    }
}
