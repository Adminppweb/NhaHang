using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Core.Areas.Core.Controllers
{
	[Area("Core")]
	[Authorize(Roles = "admin")]
	[Route("api/medias")]
	public class MediaApiController : Controller
	{
		private readonly IRepository<Media> _mediaRepository;
		private readonly IWebHostEnvironment _env;

		public MediaApiController(IRepository<Media> mediaRepository, IWebHostEnvironment env)
		{
			_mediaRepository = mediaRepository;
			_env = env;
		}

		// GET: api/medias
		[HttpGet]
		public async Task<IActionResult> GetFiles()
		{
			var mediaFiles = await _mediaRepository.Query().ToListAsync();
			return Ok(mediaFiles);
		}

		// POST: api/medias/upload
		[HttpPost("upload")]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return BadRequest("No file uploaded.");

			var uploadsFolder = Path.Combine(_env.WebRootPath, "user-content");
			if (!Directory.Exists(uploadsFolder))
				Directory.CreateDirectory(uploadsFolder);

			var filePath = Path.Combine(uploadsFolder, file.FileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			var media = new Media
			{
				Caption = file.FileName,
				FileSize = (int)file.Length,
				FileName = file.FileName,
				MediaType = MediaType.Image,
				CreatedOn = DateTimeOffset.UtcNow
			};

			_mediaRepository.Add(media);  // Phương thức đồng bộ không cần await
			await _mediaRepository.SaveChangesAsync();  // Phương thức bất đồng bộ để lưu các thay đổi


			return Ok(media);
		}

		// DELETE: api/medias/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFile(long id)
		{
			var media = await _mediaRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
			if (media == null)
				return NotFound();

			var filePath = Path.Combine(_env.WebRootPath, "user-content", media.FileName);

			if (System.IO.File.Exists(filePath))
				System.IO.File.Delete(filePath);

			_mediaRepository.Remove(media);
			await _mediaRepository.SaveChangesAsync();

			return Ok();
		}
	}
}
