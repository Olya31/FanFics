using DAL.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Manager.Interface
{
    public interface IChapterManager
    {
        Task AddChapterAsync(Chapter chapter, CancellationToken token);

        Task DeleteChapterAsync(int id, CancellationToken token);
    }
}
