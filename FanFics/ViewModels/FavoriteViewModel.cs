using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace FanFics.ViewModels
{
    public class FavoriteViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int CompositionId { get; set; }

        public CompositionViewModel Composition { get; set; }

        public Favorite ToFavorite(int compositionId, string userId)
        {
            return new Favorite
            {
                Id = this.Id,
                CompositionId = compositionId,
                UserId = userId
            };
        }
        
        public IEnumerable<FavoriteViewModel> ToFavoriteViewModel(IEnumerable<Favorite> favorites)
        {
            var compositionVM = new CompositionViewModel();

            return favorites?.Select(c => new FavoriteViewModel
            {
                Id = c.Id,
                CompositionId = c.CompositionId,
                UserId = c.UserId,
                Composition = compositionVM.ToCompositionViewModel(c.Composition),
            });
        }
    }
}
