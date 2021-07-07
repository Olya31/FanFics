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
           await _context.Composition.AddAsync(composition, token);

            _context.SaveChanges();
        }

        public async Task DeleteCompositionAsync(int id, CancellationToken token)
        {
            var item = await _context.Composition.FindAsync(new object[] { id }, token);

            if (item != null)
            {
                _context.Composition.Remove(item);
                await _context.SaveChangesAsync(token);
            }
        }
        public IEnumerable<Composition> GetCompositions()
        {
            var composition = _context.Composition;

            return composition;
        }

        public IEnumerable<Tags> GetTagsSearch(string Prefix)
        {
            var compositions = _context.Tags.ToList().Where(c => c.TagName.StartsWith(Prefix)).Select(x => new Tags
            {
                Id = x.Id,
                TagName = x.TagName
            }).ToList();

            return compositions;
        }

        public List<Composition> Sort(string sortOrder)
        {
            if(sortOrder != null)
            {
                IQueryable<Composition> composition = _context.Composition;

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

        public List<Composition> Filter(string title)
        {
            if(title != null)
            {
                IQueryable<Composition> composition = _context.Composition;
              
                if (!String.IsNullOrEmpty(title))
                {
                    composition = composition.Where(p => p.TitleComposition.Contains(title));
                }

                return composition.AsNoTracking().ToList();
                
            }
            return null;
        }

        public async Task<Composition> GetCompositionByIdAsync(int id, CancellationToken token)
        {
            var composition = await _context.Composition
                .Include(c => c.Tags)
                .SingleOrDefaultAsync(c => c.Id == id, token);

            return composition;
        }

        public void Update(Composition composition)
        {
            //_context.Composition.Remove(composition);
            //_context.SaveChanges();
            //composition.Id = 0;
            //_context.Composition.Add(composition);
            //_context.SaveChanges();
        }
    }
}
