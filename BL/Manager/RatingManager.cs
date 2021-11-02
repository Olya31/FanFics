using BL.Manager.Interface;
using DAL;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Manager
{
    public sealed class RatingManager : IRatingManager
    {
        private readonly SQLContext _context;

        public RatingManager(SQLContext context)
        {
            _context = context;
        }

        public List<Rating> GetComments (int? id)
        {
            var comments = _context.Rating.Where(d => d.CompositionId.Equals(id.Value)).ToList();

            return comments;
        }

        public List<Rating> GetRating(int? id)
        {
            var ratings = _context.Rating.Where(d => d.CompositionId.Equals(id.Value)).ToList();

            return ratings;
        }

        public async Task AddRating(Rating rating, CancellationToken token)
        {
            await _context.Rating.AddAsync(rating, token);

            _context.SaveChanges();
        }
    }
}
