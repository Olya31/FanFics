using BL.Manager.Interface;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public IEnumerable<Chapter> GetChapters()
        {
            var chapter = _context.Chapter.Include(c => c.Photo);

            return chapter;
        }

        public async Task<Chapter> GetChapterByIdAsync(int id, CancellationToken token)
        {
            var chapter = await _context.Chapter
                .Include(c => c.Photo)
                .SingleOrDefaultAsync(c => c.Id == id, token);

            return chapter;
        }

        public void Update(Chapter chapter)
        {
            var chapterDb = _context.Chapter.Find(chapter.Id);

            if (chapterDb != null)
            {
                chapterDb.Title = chapter.Title;
                chapterDb.Text = chapter.Text;
                chapterDb.PhotoId = chapter.PhotoId;
                chapterDb.Photo = chapter.Photo;
                _context.Chapter.Update(chapterDb);
                _context.SaveChanges();
            }
        }

    }
}
