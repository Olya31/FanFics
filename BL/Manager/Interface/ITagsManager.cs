using DAL.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Manager.Interface
{
    public interface ITagsManager
    {
        Task AddTagAsync(Tags tag, CancellationToken token);

        IEnumerable<Tags> GetTagsSearch(string Prefix);
    }
}
