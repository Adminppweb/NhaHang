using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Core.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly IRepository<TrackingError> _trackingRepository;
        public TrackingService(IRepository<TrackingError> trackingRepository)
        {
            _trackingRepository = trackingRepository;
        }

        public async Task TrackingError(string page, string message)
        {
            try
            {
                TrackingError traking = new TrackingError();
                traking.Page = page;
                traking.Message = message;
                traking.CreatedDate = DateTime.Now;
                _trackingRepository.Add(traking);
                _trackingRepository.SaveChanges();
            }
            catch (Exception ex)
            {
            }


        }

        public async Task<IList<TrackingError>> GetAll(string ids = null)
        {
            try
            {
                var list = await _trackingRepository.Query().OrderByDescending(x => x.Id).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public async Task<bool> Delete(long id)
        {
            bool result = false;
            var track = await _trackingRepository.Query().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (track != null)
            {
                _trackingRepository.Remove(track);
                await _trackingRepository.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<bool> DeleteMultil(List<TrackingError> tracks)
        {
            bool result = false;
            foreach (var track in tracks) { _trackingRepository.Remove(track); result = true; }
            await _trackingRepository.SaveChangesAsync();
            return result;
        }


        public async Task<bool> DeleteMultil(string ids)
        {
            bool result = false;
            string[] arrID = null;
            if (!string.IsNullOrEmpty(ids))
            {
                arrID = ids.Split(new string[] { "#" }, StringSplitOptions.None);
            }
            if (arrID == null)
            {
                return false;
            }

            foreach (var ID in arrID)
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    var track = await _trackingRepository.Query().Where(x => x.Id == Convert.ToInt64(ID)).FirstOrDefaultAsync();
                    if (track != null)
                    {
                        _trackingRepository.Remove(track);
                    }

                }
            }
            await _trackingRepository.SaveChangesAsync();
            return result;
        }
    }
}
