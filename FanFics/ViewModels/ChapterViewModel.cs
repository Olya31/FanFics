using DAL.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FanFics.ViewModels
{
    public sealed class ChapterViewModel
    {
        public int Id { get; set; }

        public int CompositionId { get; set; }

        public string Text { get; set; }

        public IFormFile Image { get; set; }

        public int PhotoId { get; set; }

        public Photo Photo { get; set; }

        public string Title { get; set; }
       

        public Chapter ToChapter(int photoId, Photo photo = null)
        {
            return new Chapter
            {
                Id = this.Id,
                CompositionId = this.CompositionId,
                Text = this.Text,
                PhotoId = photoId,
                Title = this.Title ,
                Photo = photo
            };
        }

        public Chapter ToChapter(Photo photo)
        {
            return new Chapter
            {
                Id = this.Id,
                CompositionId = this.CompositionId,
                Text = this.Text,
                PhotoId = photo.Id,
                Title = this.Title,
                Photo = photo
            };
        }

        public ChapterViewModel ToChapterViewModel(Chapter chapter)
        {
            if (chapter == null)
            {
                return null;
            }

            return new ChapterViewModel
            {
                Id = chapter.Id,
                CompositionId = chapter.CompositionId,
                Text = chapter.Text,
                Title = chapter.Title,
                PhotoId = chapter.PhotoId,
                Photo = chapter.Photo
            };
        }

        public IEnumerable<ChapterViewModel> ToChapterViewModels(IEnumerable<Chapter> chapters)
        {
            var result = new List<ChapterViewModel>();

            foreach (var item in chapters)
            {
                result.Add(ToChapterViewModel(item));
            }

            return result;
        }
    }
}
