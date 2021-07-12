using BL.Manager.Interface;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Manager
{
    public sealed class CompositionManager : ICompositionManager
    {
        private readonly SQLContext _context;

        public CompositionManager(SQLContext context)
        {
            _context = context;
        }

        public async Task AddComposition(Composition composition, CancellationToken token)
        {
            await _context.Compositions.AddAsync(composition, token);

            _context.SaveChanges();
        }

        public async Task DeleteCompositionAsync(int id, CancellationToken token)
        {
            var item = await _context.Compositions.FindAsync(new object[] { id }, token);

            if (item != null)
            {
                _context.Compositions.Remove(item);
                await _context.SaveChangesAsync(token);
            }
        }
        public IEnumerable<Composition> GetCompositions()
        {
            return _context.Compositions;
        }

        public async Task<IEnumerable<Composition>> GetCompositionsByUser(string userId, CancellationToken token)
        {
            return await _context.Compositions
                .Include(c => c.User)
                .Where(c => c.UserId == userId)
                .ToListAsync(token); 
        }

        public IEnumerable<Tags> GetTagsSearch(string Prefix)
        {
            return _context.Tags
                .ToList()
                .Where(c => c.TagName.StartsWith(Prefix))
                .Select(x => new Tags
            {
                Id = x.Id,
                TagName = x.TagName
            }).ToList();
        }

        public List<Composition> Sort(string sortOrder, string userId)
        {
            if (sortOrder != null)
            {
                IQueryable<Composition> composition = _context.Compositions
                    .Include(c => c.User)
                    .Where(c => c.UserId == userId);

                switch (sortOrder)
                {
                    case "title_desc":
                        composition = composition.OrderByDescending(s => s.TitleComposition);
                        break;
                    case "Date":
                        composition = composition.OrderBy(s => s.DateAdded);
                        break;
                    case "date_desc":
                        composition = composition.OrderByDescending(s => s.DateAdded);
                        break;
                    case "Fandom":
                        composition = composition.OrderBy(s => s.Fandom);
                        break;
                    case "fandom_desc":
                        composition = composition.OrderByDescending(s => s.Fandom);
                        break;
                    default:
                        composition = composition.OrderBy(s => s.TitleComposition);
                        break;
                }

                return composition.AsNoTracking().ToList();

            }

            return null;
        }

        public async Task<IList<Composition>> GetToMainPageAsync(CancellationToken token)
        {
            return await _context.Compositions
                .Include(c => c.Rating)
                .OrderByDescending(c => c.DateUpdate)
                .ThenByDescending(c => c.Rating.Average(c => c.RatingCounter))
                .ToListAsync(token);
        }

        public async Task<IList<Composition>> FilterAsync(string title, string userId, CancellationToken token)
        {
            if (title != null && !String.IsNullOrEmpty(title))
            {
                return await _context.Compositions
                    .Where(c => c.UserId == userId)
                    .Where(p => p.TitleComposition.Contains(title))
                    .ToListAsync(token);
            }

            return null;
        }

        public async Task<Composition> GetCompositionByIdAsync(int id, CancellationToken token)
        {
            return await _context.Compositions
                .Include(c => c.Tags)
                .Include(c => c.Chapters)
                .SingleOrDefaultAsync(c => c.Id == id, token);
        }

        public async Task EditCompositionAsync(Composition composition, CancellationToken token)
        {
            var compositionDb = await _context.Compositions.FindAsync(composition.Id, token);

            if (compositionDb != null)
            {
                compositionDb.Author = composition.Author;
                compositionDb.DateAdded = composition.DateAdded;
                compositionDb.DateUpdate = DateTime.Now;
                compositionDb.Fandom = composition.Fandom;
                compositionDb.TitleComposition = composition.TitleComposition;
                compositionDb.ShortDescription = composition.ShortDescription;
                compositionDb.Tags = composition.Tags;
                compositionDb.Chapters = composition.Chapters;
                _context.Compositions.Update(compositionDb);
                await _context.SaveChangesAsync(token);
            }
        }
    }
}
