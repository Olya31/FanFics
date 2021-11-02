using DAL.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Manager.Interface
{
    public interface IRatingManager
    {
        List<Rating> GetComments(int? id);

        List<Rating> GetRating(int? id);

        Task AddRating(Rating rating, CancellationToken token);
    }
}
