using DAL.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Manager.Interface
{
    public interface IFavoriteManager
    {
        Task AddFavoriteAsync(Favorite favorite, CancellationToken token);

        Task<IList<Favorite>> GetFavoriteByUserIdAsync(string userId, CancellationToken token);

        Task DeleteFavoriteAsync(int id, CancellationToken token);
    }
}
