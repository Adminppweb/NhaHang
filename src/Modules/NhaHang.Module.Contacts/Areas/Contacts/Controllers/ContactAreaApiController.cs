using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaHang.Infrastructure.Commons;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Contacts.Areas.Contacts.ViewModels;
using NhaHang.Module.Contacts.Models;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Contacts.Areas.Contacts.Controllers
{
    [Area("Contacts")]
    [Authorize(Roles = "admin")]
    [Route("api/contact-area")]
    public class ContactAreaApiController : Controller
    {
        private readonly IRepository<ContactArea> _contactRepository;
		private readonly ITrackingService _trackingService;

		public ContactAreaApiController(IRepository<ContactArea> contactRepository,
			ITrackingService _trackingService)
        {
            _contactRepository = contactRepository;
			this._trackingService = _trackingService;
		}

        [HttpGet]
        public IActionResult Get()
        {
            var contactList = _contactRepository.Query().Where(x => !x.IsDeleted).ToList();
            return Json(contactList);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var contact = _contactRepository.Query().FirstOrDefault(x => x.Id == id);
            var model = new ContactAreaForm
            {
                Id = contact.Id,
                Name = contact.Name
            };

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactAreaForm model)
        {
			CustomJsonResult customJsonResult = new CustomJsonResult();
			try
			{
				if (!ModelState.IsValid)
				{
					//return BadRequest(ModelState);
					await _trackingService.TrackingError("contact", "BadRequest");
					return Ok("Fail !!!");
				}
				var contact = new ContactArea
				{
					Name = model.Name
				};

				_contactRepository.Add(contact);
				_contactRepository.SaveChanges();

				customJsonResult.Result = contact.Id;
				customJsonResult.StatusCode = 200;
			}
			catch (Exception ex)
			{
				await _trackingService.TrackingError("contact", ex.Message);
				customJsonResult.StatusCode = 502;
			}
			return Ok(customJsonResult);
		}

        [HttpPost("update-contact-area/{id}")]
        public IActionResult Put(long id, [FromBody] ContactAreaForm model)
        {
			CustomJsonResult customJsonResult = new CustomJsonResult();
			try
			{
                var contact = _contactRepository.Query().FirstOrDefault(x => x.Id == id);
                contact.Name = model.Name;

                _contactRepository.SaveChanges();

				customJsonResult.Result = contact.Id;
				customJsonResult.StatusCode = 200;
			}
			catch (Exception ex)
			{
				_trackingService.TrackingError("PUT Contact", ex + string.Empty);
				customJsonResult.StatusCode = 502;
			}
			//return BadRequest();
			return Ok(customJsonResult);
		}

        [HttpPost("delete-contact-area/{id}")]
        public IActionResult Delete(long id)
        {
            var contact = _contactRepository.Query().FirstOrDefault(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            contact.IsDeleted = true;
            _contactRepository.SaveChanges();

            return Json(true);
        }
    }
}
