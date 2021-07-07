using BL.Manager.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DAL.Models;
using FanFics.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FanFics.Controllers
{
    public sealed class ChapterController : Controller
    {
        private readonly ILogger<CompositionController> _logger;
        private readonly IChapterManager _chapterManager;
        private readonly Cloudinary _cloudinary;
        private readonly IPhotoManager _photoManager;

        public ChapterController(
            ILogger<CompositionController> logger,
            IChapterManager chapterManager,
            Cloudinary cloudinary,
            IPhotoManager photoManager)
        {
            _logger = logger;
            _chapterManager = chapterManager;
            _cloudinary = cloudinary;
            _photoManager = photoManager;
        }

        [HttpGet]
        public ActionResult Index()
        {            
            return View("Add");
        }

        [HttpGet]
        public IActionResult Image(int idChapter)
        {
            return View("AddImage", idChapter);
        }

        [HttpPost]
        public async Task<IActionResult> Add(
            ChapterViewModel chapter,
            CancellationToken token)
        {
            var photoId = await UploadPhotoAsync(chapter.Image, token);
            var model = chapter.ToChapter(photoId ?? 0);
            await _chapterManager.AddChapterAsync(model, token);

            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int? id, CancellationToken token)
        {
            if (id.HasValue)
            {
                await _chapterManager.DeleteChapterAsync(id.Value, token);

                return RedirectToAction("Index", "Profile");
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
    }
}

