using DAL.Models;
using System.Collections.Generic;

namespace FanFics.ViewModels
{
    public class ReadonlyViewModel
    {
        public CompositionViewModel Composition { get; set; }

        public RatingViewModel Rating  { get; set; }

        public User User { get; set; }
    }
}
