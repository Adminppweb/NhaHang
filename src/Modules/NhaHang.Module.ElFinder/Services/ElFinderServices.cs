using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.ElFinder.Services
{
    public class ElFinderServices : IElFinderServices
    {
        private readonly IRepository<Media> _repositoryMedia;
        public ElFinderServices(IRepository<Media> _repositoryMedia) { this._repositoryMedia = _repositoryMedia; }
        //// save file/folder to database
        public async Task SaveMediaAsync(FullPath fullPath, string name)
        {
            Media media = null;
            var fileName = Path.Combine(fullPath.Directory.FullName, name);
            //// check exist file
            var mediaExist = _repositoryMedia.Query().Any(x => x.FileName == fileName);
            if (!mediaExist)
            {
                media = new Media { FileName = fileName, Caption = name };
                _repositoryMedia.Add(media);
            }
            else
            {

            }
            await _repositoryMedia.SaveChangesAsync();
        }
    }
}
