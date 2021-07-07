using DAL.Models;
using System.Collections.Generic;

namespace FanFics.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public IEnumerable<CompositionViewModel> Compositions { get; set; }
    }
}
