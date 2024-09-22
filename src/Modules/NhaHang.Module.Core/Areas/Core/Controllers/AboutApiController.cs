
using NhaHang.Module.Core.Areas.Core.ViewModels;
using NhaHang.Module.Core.Models;
//using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NhaHang.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace NhaHang.Module.Core.Areas.Core.Controllers
{
    [Area("Core")]
    [Authorize(Roles = "admin")]
    [Route("api/abouts")]
    [ApiController]
    public class AboutApiController : ControllerBase
    {
        private readonly IRepository<About> _aboutRepository;
        //private readonly IConfigurationRoot _configurationRoot;
        //private readonly IMapper _mapper;
        public AboutApiController(IRepository<About> aboutRepository, IConfiguration configuration)
        {
            _aboutRepository = aboutRepository;
            //_configurationRoot = (IConfigurationRoot)configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                AboutVm aboutVm = null;
                var settings = await _aboutRepository.Query().FirstOrDefaultAsync();
                if (settings == null)
                {
                    aboutVm = new AboutVm();
                }
                else
                {
                    aboutVm = new AboutVm()
                    {
                        Id = settings.Id,
                        Address = settings.Address,
                        Facebook = settings.Facebook,
                        Instagram = settings.Instagram,
                        Messenger = settings.Messenger,
                        Twiter = settings.Twiter,
                        Hotline = settings.Hotline,
                        Phone = settings.Phone,
                        Fax = settings.Fax,
						TaxCode = settings.TaxCode,
						Description = settings.Description,
						Slogan = settings.Slogan,
                        Email = settings.Email,
                        Close = settings.Close,
                        Open = settings.Open,
                        Pinterest = settings.Pinterest,
                        Youtube = settings.Youtube,
                        Zalo = settings.Zalo,
                        Linkedin = settings.Linkedin,
                        Map = settings.Map,
                        Tiktok = settings.Tiktok,
                        GoogleAnalytics = settings.GoogleAnalytics,
                        FacebookPixel = settings.FacebookPixel,
                        GoogleTag = settings.GoogleTag,
                        GoogleAds = settings.GoogleAds,
                        FacebookFanpage = settings.FacebookFanpage,
                        FacebookLibrary = settings.FacebookLibrary
                    };
                }
                return Ok(aboutVm);
            }
            catch (System.Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Put([FromBody] AboutVm model)
        {
            if (ModelState.IsValid) //?? sao lại chỉnh lại
            {
                try
                {
                    bool IsUpdate = true;
                    var abouts = await _aboutRepository.Query().Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (abouts == null)
                    {
                        IsUpdate = false;
                        abouts = new About();
                    }

                    abouts.Phone = model!.Phone;
                    abouts.Hotline = model!.Hotline;
                    abouts.Address = model!.Address;
                    abouts.Email = model!.Email;
                    abouts.Fax = model!.Fax;
                    abouts.TaxCode = model!.TaxCode;
                    abouts.Zalo = model!.Zalo;
                    abouts.Facebook = model!.Facebook;
                    abouts.Messenger = model!.Messenger;
                    abouts.Twiter = model!.Twiter;
                    abouts.Instagram = model.Instagram;
                    abouts.Youtube = model!.Youtube;
                    abouts.Pinterest = model!.Pinterest;
                    abouts.Description = model!.Description;
                    abouts.Slogan = model!.Slogan;
                    abouts.Linkedin = model!.Linkedin;
                    abouts.Open = model!.Open;
                    abouts.Close = model!.Close;
                    abouts.Tiktok = model!.Tiktok;
                    abouts.Map = model!.Map;
                    abouts.GoogleAnalytics = model!.GoogleAnalytics;
                    abouts.FacebookPixel = model!.FacebookPixel;
                    abouts.GoogleTag = model!.GoogleTag;
                    abouts.GoogleAds = model!.GoogleAds;
                    abouts.FacebookFanpage = model!.FacebookFanpage;
                    abouts.FacebookLibrary = model!.FacebookLibrary;
                    if (!IsUpdate)
                    {
                        _aboutRepository.Add(abouts);
                    }

                    await _aboutRepository.SaveChangesAsync();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest(ModelState);
                }
                // _configurationRoot.Reload();
                return Accepted();
            }
            return Accepted();

        }
    }
}