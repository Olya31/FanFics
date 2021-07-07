using BL.Manager.Interface;
using DAL;
using DAL.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Manager
{
    public sealed class ChapterManager : IChapterManager
    {
        private readonly SQLContext _context;

        public ChapterManager(SQLContext context)
        {
            _context = context;
        }

        public async Task AddChapterAsync(Chapter chapter, CancellationToken token)
        {
            await _context.Chapter.AddAsync(chapter, token);

            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteChapterAsync(int id, CancellationToken token)
        {
            var item = await _context.Chapter.FindAsync(new object[] { id }, token);

            if (item != null)
            {
                _context.Chapter.Remove(item);
                await _context.SaveChangesAsync(token);
            }
        }
    }
}
