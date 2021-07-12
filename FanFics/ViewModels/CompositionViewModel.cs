using DAL.Models;
using System;
using System.Collections.Generic;

namespace FanFics.ViewModels
{
    public class CompositionViewModel
    {
        public int Id { get; set; }

        public string TitleComposition { get; set; }

        public string ShortDescription { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public string Fandom { get; set; }

        public DateTime DateUpdate { get; set; }

        public string Author { get; set; }  
        
        public List<Tags> Tags { get; set; }

        public List<Chapter> Chapters { get; set; }

        public string UserId { get; set; }

        public bool IsFavorite { get; set; }

        public List<Rating> Ratings { get; set; }

        public Composition ToComposition(string userId)
        {
            return new Composition
            {
                Id = this.Id,
                TitleComposition = this.TitleComposition,
                ShortDescription = this.ShortDescription,
                DateAdded = this.DateAdded,
                Fandom = this.Fandom,
                DateUpdate = this.DateUpdate,
                Author = this.Author,
                Tags = this.Tags,
                UserId = userId,
            };
        }

        public Composition ToComposition(CompositionViewModel composition)
        {
            return new Composition
            {
                Id = composition.Id,
                TitleComposition = composition.TitleComposition,
                ShortDescription = composition.ShortDescription,
                DateAdded = composition.DateAdded,
                Fandom = composition.Fandom,
                DateUpdate = DateTime.Now,
                Author = composition.Author,
                Tags = composition.Tags,
                UserId = composition.UserId,
            };
        }

        public CompositionViewModel ToCompositionViewModel(Composition composition)
        {
            if (composition == null)
            {
                return null;
            }

            return new CompositionViewModel
            {
                Id = composition.Id,
                TitleComposition = composition.TitleComposition,
                ShortDescription = composition.ShortDescription,
                DateAdded = composition.DateAdded,
                Fandom = composition.Fandom,
                DateUpdate = DateTime.Now,
                Author = composition.Author,
                Tags = composition.Tags,
                Chapters = composition.Chapters,
                IsFavorite = false,
                Ratings = composition.Rating,
                UserId = composition.UserId,
            };
        }
        public IEnumerable<CompositionViewModel> ToCompositionViewModels(IEnumerable<Composition> compositions)
        {
            var result = new List<CompositionViewModel>();

            foreach (var item in compositions)
            {
                result.Add(ToCompositionViewModel(item));
            }

            return result;
        }
    }
}
