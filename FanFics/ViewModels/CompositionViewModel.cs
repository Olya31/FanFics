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

        public DateTime DateUpDate { get; set; }

        public string Author { get; set; }  
        
        public List<Tags> Tags { get; set; }

        public Composition ToComposition()
        {
            return new Composition
            {
                Id = this.Id,
                TitleComposition = this.TitleComposition,
                ShortDescription = this.ShortDescription,
                DateAdded = this.DateAdded,
                Fandom = this.Fandom,
                DateUpDate = this.DateUpDate,
                Author = this.Author,
                Tags = this.Tags,
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
                DateUpDate = composition.DateUpDate,
                Author = composition.Author,
                Tags = composition.Tags,
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
