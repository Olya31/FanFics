using BL.Manager.Interface;
using DAL;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Manager
{
    public sealed class TagsManager : ITagsManager
    {
        private readonly SQLContext _context;

        public TagsManager(SQLContext context)
        {
            _context = context;
        }

        public async Task AddTagAsync(Tags tag, CancellationToken token)
        {
            await _context.Tags.AddAsync(tag, token);

            _context.SaveChanges();
        }

        public IEnumerable<Tags> GetTagsSearch(string Prefix)
        {
            var tags = _context.Tags.ToList().Where(c => c.TagName.StartsWith(Prefix)).Select(x => new Tags
            {
                Id = x.Id,
                TagName = x.TagName
            }).ToList();

            return tags;
        }
    }
}
