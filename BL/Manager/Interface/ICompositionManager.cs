using DAL.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Manager.Interface
{
    public interface ICompositionManager
    {
        Task AddComposition(Composition composition, CancellationToken token);

        Task DeleteCompositionAsync(int id, CancellationToken token);

        IEnumerable<Composition> GetCompositions();

        Task<IList<Composition>> FilterAsync(string title, string userId, CancellationToken token);

        List<Composition> Sort(string sortOrder, string userId);

        Task<Composition> GetCompositionByIdAsync(int id, CancellationToken token);

        Task EditCompositionAsync(Composition composition, CancellationToken token);

        Task<IList<Composition>> GetToMainPageAsync(CancellationToken token);

        Task<IEnumerable<Composition>> GetCompositionsByUser(string userId, CancellationToken token);
    }
}
