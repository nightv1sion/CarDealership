using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Presentation.Contollers
{
    [ApiController]
    [Route("api/photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IServiceManager _service;
        public PhotoController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet("dealershop/{id}")]
        public async Task<IActionResult> GetPhotosForDealerShop(Guid id)
        {
            var photos = await _service.PhotoForDealerShopService.GetPhotosAsync(id, false);

            return Ok(photos);
        }

        [HttpDelete("dealershop/{photoId}")]
        public async Task<IActionResult> DeletePhotoForDealerShop(Guid photoId)
        {
            await _service.PhotoForDealerShopService.DeletePhotoAsync(photoId);
            return NoContent();
        }
    }
}
