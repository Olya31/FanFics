using DAL.Models;

namespace FanFics.ViewModels
{
    public sealed class TagsViewModel
    {
        public int Id { get; set; }

        public string TagName { get; set; }

        public Tags ToTag() => new Tags
        {
            Id = this.Id,
            TagName = this.TagName            
        };
    }
}
