using System.Collections.Generic;
using System.Threading.Tasks;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Core.Services
{
    public interface ITrackingService
    {
        Task TrackingError(string page, string message);

        Task<IList<TrackingError>> GetAll(string ids = null);
        Task<bool> Delete(long id);
        Task<bool> DeleteMultil(List<TrackingError> tracks);

        Task<bool> DeleteMultil(string ids);
    }
}
