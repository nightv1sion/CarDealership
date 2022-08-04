using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Presentation.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerShopController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IMapper _mapper;
        public DealerShopController(IMapper mapper, IServiceManager service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetDealerShops()
        {
            var companies = _service.DealerShopService.GetAllDealerShops(false);
            return Ok(companies);
        }

        /*[HttpPost]
        public async Task<IActionResult> CreateDealerShopAsync([FromForm] DealerShopCreationDTO dealerShopDTO)
        {
            if (dealerShopDTO == null)
                return BadRequest("Received data is null");
            if (_dealerShopRepository.DealerShopExistsByOrdinalNumber(dealerShopDTO.OrdinalNumber))
                return BadRequest("This Ordinal Number already exists");

            var dealerShop = _mapper.Map<DealerShop>(dealerShopDTO);
            dealerShop.DealerShopId = Guid.NewGuid();

            dealerShop.Photos = new List<PhotoForDealerShop>();
            dealerShop.Cars = new List<Car>();
            dealerShop.Location = ConvertLocation(dealerShopDTO.Location);

            foreach (var file in dealerShopDTO.Files)
            {
                dealerShop.Photos.Add(ConvertFileToPhoto(file, dealerShop));
            }

            _dealerShopRepository.Add(dealerShop);

            await _dealerShopRepository.SaveAsync();

            return Ok("Dealer shop successfully created");
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteDealerShopAsync(Guid id)
        {
            if (!_dealerShopRepository.DealerShopExists(id))
                return NotFound();

            var dealerShop = _dealerShopRepository.GetDealerShop(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dealerShopRepository.Delete(dealerShop);
            await _dealerShopRepository.SaveAsync();

            return Ok("Dealershop was successfully deleted");
        }


        [HttpPut]
        public async Task<IActionResult> EditDealerShopAsync([FromForm] DealerShopDTO dealerShopDTO)
        {
            if (dealerShopDTO == null)
                return BadRequest("Received data is null");

            var dealerShop = _mapper.Map<DealerShop>(dealerShopDTO);
            dealerShop.DealerShopId = Guid.Parse(dealerShopDTO.DealerShopId);

            if (_dealerShopRepository.DealerShopExistsByOrdinalNumber(dealerShopDTO.OrdinalNumber, dealerShop.DealerShopId))
                return BadRequest("This Ordinal Number already exists");

            dealerShop.Location = ConvertLocation(dealerShopDTO.Location);

            if (dealerShopDTO.Files != null)
            {
                var photos = new List<PhotoForDealerShop>();
                foreach (var file in dealerShopDTO.Files)
                {
                    photos.Add(ConvertFileToPhoto(file, dealerShop));
                }
                _photoRepository.DeleteRange(_dealerShopRepository.GetPhotoOfAnDealerShop(dealerShop.DealerShopId).ToList());
                await _photoRepository.AddRangeAsync(photos);
            }

            _dealerShopRepository.Update(dealerShop);
            await _dealerShopRepository.SaveAsync();
            return Ok("Dealershop was successfully updated");
        }

        */

        private PhotoForDealerShop ConvertFileToPhoto(IFormFile file, DealerShop dealerShop)
        {
            var photo = new PhotoForDealerShop()
            {
                PhotoId = Guid.NewGuid(),
                Description = file.FileName,
                Size = file.Length,
                DealerShop = dealerShop,
                DealerShopId = dealerShop.DealerShopId,
            };
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    photo.Bytes = fileBytes;
                }

            }
            return photo;
        }

        private Point ConvertLocation(string location)
        {
            var numsOfLocation = location.Split(", ");
            double coordX = double.Parse(numsOfLocation[0], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
            double coordY = double.Parse(numsOfLocation[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
            return new Point(coordX, coordY) { SRID = 4326 };
        }
    }
}
