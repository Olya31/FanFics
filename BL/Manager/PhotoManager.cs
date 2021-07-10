using BL.Manager.Interface;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Manager
{
    public sealed class PhotoManager : IPhotoManager
    {
        private readonly SQLContext _context;

        public PhotoManager(SQLContext context)
        {
            _context = context;
        }

        public async Task<int> AddPhotoAsync(Photo photo, CancellationToken cancellationToken)
        {
            await _context.Photos.AddAsync(photo, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return photo.Id;
        }

        //public async Task<int> GetPhotodAsync(int photoId, CancellationToken cancellationToken)
        //{
        //    var photo = await _context.Photos.Find(new object[] { photoId }, cancellationToken);

        //    return photo.Id;
        //}

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            var photo = await _context.Photos.FindAsync(new object[] { id }, token);

            if (photo != null)
            {
                _context.Remove(photo);
            }
        }

        public async Task<Photo> GetPhotoAsync(int id, CancellationToken token)
        {
            var photo = await _context.Photos
                .Include(c => c.Chapter)
                .SingleOrDefaultAsync(c => c.Id == id, token);

            return photo;
        }

    }
}
