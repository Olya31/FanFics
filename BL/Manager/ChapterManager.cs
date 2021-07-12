using BL.Manager.Interface;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
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
            await _context.Chapters.AddAsync(chapter, token);

            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteChapterAsync(int id, CancellationToken token)
        {
            var item = await _context.Chapters.FindAsync(new object[] { id }, token);

            if (item != null)
            {
                _context.Chapters.Remove(item);
                await _context.SaveChangesAsync(token);
            }
        }

        public async Task<Chapter> GetChapterByIdAsync(int id, CancellationToken token)
        {
            var chapter = await _context.Chapters
                .Include(c => c.Photo)
                .SingleOrDefaultAsync(c => c.Id == id, token);

            return chapter;
        }

        public async Task UpdateAsync(Chapter chapter)
        {
            var chapterDb = await _context.Chapters.FindAsync(chapter.Id);

            if (chapterDb != null)
            {
                chapterDb.Title = chapter.Title;
                chapterDb.Text = chapter.Text;
                chapterDb.PhotoId = chapter.PhotoId;
                chapterDb.Photo = chapter.Photo;
                 _context.Chapters.Update(chapterDb);
                await _context.SaveChangesAsync();
            }
        }
    }
}
