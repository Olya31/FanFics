using BL.Manager.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DAL.Models;
using FanFics.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FanFics.Controllers
{
    [Authorize]
    public sealed class ChapterController : Controller
    {
        private readonly ILogger<CompositionController> _logger;
        private readonly IChapterManager _chapterManager;
        private readonly ICompositionManager _compositionManager;
        private readonly Cloudinary _cloudinary;
        private readonly IPhotoManager _photoManager;

        public ChapterController(
            ILogger<CompositionController> logger,
            IChapterManager chapterManager,
            Cloudinary cloudinary,
            IPhotoManager photoManager,
            ICompositionManager compositionManager)
        {
            _logger = logger;
            _chapterManager = chapterManager;
            _cloudinary = cloudinary;
            _photoManager = photoManager;
            _compositionManager = compositionManager;
        }

        [HttpGet]
        public ActionResult Index(CompositionViewModel composition)
        {
            ViewBag.Composition = composition.Id;
            return View("Add");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken token)
        {
            if (id.HasValue)
            {
                var chapterViewModels = new ChapterViewModel();
                var chapter = await _chapterManager.GetChapterByIdAsync(id.Value, token);
                var chapterViewModel = chapterViewModels.ToChapterViewModel(chapter);

                return View("edit", chapterViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ChapterViewModel chapter, CancellationToken token)
        {
            var photoId = await UploadPhotoAsync(chapter.Image, token);
            if (photoId.HasValue)
            {
                await _photoManager.DeleteAsync(chapter.PhotoId, token);
                var photo = await _photoManager.GetPhotoAsync(photoId.Value, token);
                await _chapterManager.UpdateAsync(chapter.ToChapter(photo));
            }
            else
            {
                var photo = await _photoManager.GetPhotoAsync(chapter.PhotoId, token);
                await _chapterManager.UpdateAsync(chapter.ToChapter(photo));
            }

            return RedirectToAction("Chapters", new { compositionId = chapter.CompositionId });
        }

        [HttpGet]
        public async Task<ActionResult> Chapters(int? compositionId, CancellationToken token)
        {
            if (compositionId.HasValue)
            {
                var chapterViewModels = new ChapterViewModel();
                var composition = await _compositionManager.GetCompositionByIdAsync(compositionId.Value, token);
                var chapters = composition.Chapters;
                var chapterViewModel = chapterViewModels.ToChapterViewModels(chapters);

                return View("Chapters", chapterViewModel);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> Add(
            ChapterViewModel chapter,
            CancellationToken token)
        {
            var photoId = await UploadPhotoAsync(chapter.Image, token);
            var model = chapter.ToChapter(photoId ?? 0);
            await _chapterManager.AddChapterAsync(model, token);

            return RedirectToAction("Edit", "Composition", new { id = model.CompositionId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int? id, int? compositionId, CancellationToken token)
        {
            if (id.HasValue)
            {
                await _chapterManager.DeleteChapterAsync(id.Value, token);

                return RedirectToAction("Chapters", new { compositionId = compositionId.Value });
            }

            return NotFound();
        }

        private async Task<int?> UploadPhotoAsync(
            IFormFile image,
            CancellationToken cancellationToken)
        {
            if (image == null || image.Length == 0)
            {
                return null;
            }

            var result = await _cloudinary.UploadAsync(new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream()),
            });

            var phote = new Photo
            {
                Bytes = (int)result.Bytes,
                CreatedAt = DateTime.Now,
                Format = result.Format,
                Path = result.Url.AbsolutePath,
                PublicId = result.PublicId,
                ResourceType = result.ResourceType,
                SecureUrl = result.SecureUrl.AbsoluteUri,
                Signature = result.Signature,
                Type = result.JsonObj["type"]?.ToString(),
                Url = result.Url.AbsoluteUri,
            };

            return await _photoManager.AddPhotoAsync(phote, cancellationToken);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Read(int? id, CancellationToken token)
        {
            if (id.HasValue)
            {
                var chapterViewModels = new ChapterViewModel();
                var chapter = await _chapterManager.GetChapterByIdAsync(id.Value, token);
                var chapterViewModel = chapterViewModels.ToChapterViewModel(chapter);

                return View("Read", chapterViewModel);
            }

            return NotFound();

        }
    }
}

