using BL.Manager.Interface;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Manager
{
    public sealed class FavoriteManager : IFavoriteManager
    {
        private readonly SQLContext _context;

        public FavoriteManager(SQLContext context)
        {
            _context = context;
        }

        public async Task DeleteFavoriteAsync(int id, CancellationToken token)
        {
            var item = await _context.Favorites.FindAsync(new object[] { id }, token);

            if (item != null)
            {
                _context.Favorites.Remove(item);
                await _context.SaveChangesAsync(token);
            }
        }

        public async Task AddFavoriteAsync(Favorite favorite, CancellationToken token)
        {
            await _context.Favorites.AddAsync(favorite, token);

            _context.SaveChanges();
        }

        public async Task<IList<Favorite>> GetFavoriteByUserIdAsync(string userId, CancellationToken token)
        {
            return await _context.Favorites
                .Include(c => c.Composition)
                .Where(c => c.UserId == userId)
                .ToListAsync(token);
        }
    }
}
