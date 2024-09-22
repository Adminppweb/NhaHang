using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Core.Models;
using NhaHang.Module.ElFinder;
using NhaHang.Module.ElFinder.Helpers;
using NhaHang.Module.ElFinder.Services;

namespace NhaHang.Module.ElFinder.Areas.ElFinder.Controllers
{
	[Area("ElFinder")]
	[AllowAnonymous]
	[Route("el-finder/file-system")]
	[ApiController]
	public class FileSystemController : Controller
	{
		private readonly string BaseDirectory = "";

		private readonly string ManagerFolder = "user-content";

		private readonly IWebHostEnvironment _env;
		private readonly IElFinderServices _finderService;

		public FileSystemController(IWebHostEnvironment env, IElFinderServices finderService)
		{
			_env = env; BaseDirectory = env.WebRootPath; _finderService = finderService;
		}

		[HttpGet("connector")]
		public async Task<IActionResult> Connector()
		{
			try
			{
				////var connector = ElFinderHelper.GetConnector(Request.Scheme, Request.Host, ManagerFolder);
				var connector = ElFinderHelper.GetConnectorV1(Request.Scheme, Request.Host, _finderService, ManagerFolder);
				var Process = await connector.ProcessAsync(Request);
				return Process;
			}
			catch (Exception ex)
			{
				return Content("{}");
				////return BadRequest(ex);
			}
		}

		[HttpPost("UploadFile")]
		public async Task<IActionResult> UploadFile()
		{
			try
			{
				var connector = ElFinderHelper.GetConnector(Request.Scheme, Request.Host, files: ManagerFolder);
				var Process = await connector.ProcessAsync(Request);
				//// xử lý việt log to Media

				return Process;
			}
			catch (Exception ex)
			{
				return Error.ThreadsToRuning();
			}
		}

		public bool IsReusable
		{
			get { return false; }
		}

		[HttpGet("thumb/{hash}")]
		public async Task<IActionResult> Thumbs(string hash)
		{
			var connector = ElFinderHelper.GetConnector(Request.Scheme, Request.Host, ManagerFolder);
			return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
		}

		[HttpGet("file/{hash}")]
		public async Task<IActionResult> GetFiles(string hash)
		{
			var connector = ElFinderHelper.GetConnector(Request.Scheme, Request.Host, ManagerFolder);
			//// var files = await connector.GetFileByHashAsync(hash);
			var files = await connector.GetThumbnailAsync(hash);
			return Ok(files);
		}
	}
}