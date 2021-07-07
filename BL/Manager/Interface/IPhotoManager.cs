using DAL.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Manager.Interface
{
    public interface IPhotoManager
    {
        Task<int> AddPhotoAsync(Photo photo, CancellationToken cancellationToken);
    }
}
