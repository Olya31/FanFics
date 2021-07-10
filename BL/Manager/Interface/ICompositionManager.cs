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

        List<Composition> Filter(string title);

        List<Composition> Sort(string sortOrder);

        Task<Composition> GetCompositionByIdAsync(int id, CancellationToken token);

        void EditComposition(Composition composition);

        //void Update(Composition composition);
    }
}
