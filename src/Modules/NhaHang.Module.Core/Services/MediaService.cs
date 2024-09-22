using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Commons;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Core.Services
{
    public class MediaService : IMediaService
    {
        private readonly IRepository<Media> _mediaRepository;
        private readonly IStorageService _storageService;
        private readonly ITrackingService _trackingService;
        private const string MediaRootFoler = "user-content";
        public MediaService(IRepository<Media> mediaRepository, IStorageService storageService, ITrackingService trackingService)
        {
            _mediaRepository = mediaRepository;
            _storageService = storageService;
            _trackingService = trackingService;
        }

        //Create Folder News
        public async Task<CustomJsonResult> CreateDirectory(string fileName, string caption = "")
        {
            CustomJsonResult customJson = new CustomJsonResult();
            try
            {
                var existFolder = _mediaRepository.Query().Where(x => x.MediaType == MediaType.Folder && x.FileName == fileName).Any();
                if (existFolder)
                {
                    fileName = fileName + "(copy)";
                    caption = caption + "(copy)";
                }

                //// create folder new at storage
                var result = await _storageService.CreateDirectory(fileName);
                if (result.StatusCode != 200)
                {
                    customJson.Message = "Cannot create folder !!!";
                    customJson.StatusCode = 500;
                    return customJson;
                }
                //// var mediaPerent = _mediaRepository.Query().Where(x => x.MediaType == MediaType.Folder && x.ParentId.GetValueOrDefault()).FirstOrDefault();
                //add imformation of folder to database
                Media mediaNew = new Media()
                {
                    FileName = fileName,
                    FileSize = 0,
                    CreatedOn = DateTimeOffset.Now,
                    MediaType = MediaType.Folder,
                    Caption = caption,
                    //// ParentId = mediaPerent == null ? null : mediaPerent.Id,
                };
                _mediaRepository.Add(mediaNew);
                _mediaRepository.SaveChanges();
                customJson.StatusCode = 200;
                customJson.Result = mediaNew;
                return customJson;
            }
            catch (Exception ex)
            {
                customJson.StatusCode = 500;
                customJson.Message = "Bad Request";
                await _trackingService.TrackingError("CreateFolderNew", ex.ToString());
                return customJson;
            }
        }

        //Create Folder News
        public async Task<CustomJsonResult> CreateFolderNew(string fileName, string caption = "", string pathParent = "")
        {
            CustomJsonResult customJson = new CustomJsonResult();
            try
            {
                var existFolder = _mediaRepository.Query().Where(x => x.MediaType == MediaType.Folder && x.FileName == fileName).Any();
                if (existFolder)
                {
                    fileName = fileName + "(copy)";
                    caption = caption + "(copy)";
                }

                //// create folder new at storage
                var pathFile = await _storageService.CreateFolderNew(fileName);
                if (string.IsNullOrEmpty(pathFile))
                {
                    customJson.Message = "Cannot create folder !!!";
                    customJson.StatusCode = 500;
                    return customJson;
                }
                var mediaPerent = _mediaRepository.Query().Where(x => x.MediaType == MediaType.Folder && x.FileName == pathParent).FirstOrDefault();

                //add imformation of folder to database
                Media mediaNew = new Media()
                {
                    FileName = fileName,
                    FileSize = 0,
                    CreatedOn = DateTimeOffset.Now,
                    MediaType = MediaType.Folder,
                    Caption = caption,
                    ParentId = mediaPerent == null ? null : mediaPerent.Id,
                };
                _mediaRepository.Add(mediaNew);
                _mediaRepository.SaveChanges();
                customJson.StatusCode = 200;
                customJson.Result = mediaNew;
                return customJson;
            }
            catch (Exception ex)
            {
                customJson.StatusCode = 500;
                customJson.Message = "Bad Request";
                await _trackingService.TrackingError("CreateFolderNew", ex.ToString());
                return customJson;
            }
        }


        public Media GetMediaId(long Id)
        {
            if (Id < 0 || Id == null)
            {
                return null;
            }

            var media = _mediaRepository.Query().FirstOrDefault(x => x.Id == Id);
            return media;
        }

        public string GetMediaUrl(Media media)
        {
            if (media == null)
            {
                return GetMediaUrl("no-image.png");
            }

            return GetMediaUrl(media.FileName);
        }

        public string GetMediaUrl(string fileName)
        {
            return _storageService.GetMediaUrl(fileName);
        }

        public string GetThumbnailUrl(Media media)
        {
            return GetMediaUrl(media);
        }

        public Task SaveMediaAsync(Stream mediaBinaryStream, string fileName, string mimeType = null)
        {
            return _storageService.SaveMediaAsync(mediaBinaryStream, fileName, mimeType);
        }

        public Task DeleteMediaAsync(Media media)
        {
            _mediaRepository.Remove(media);
            return DeleteMediaAsync(media.FileName);
        }

        public Task DeleteMediaAsync(string fileName)
        {
            return _storageService.DeleteMediaAsync(fileName);
        }
    }
}
