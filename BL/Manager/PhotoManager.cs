using BL.Manager.Interface;
using DAL;
using DAL.Models;
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
    }
}
