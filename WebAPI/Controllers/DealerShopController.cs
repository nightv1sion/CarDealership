using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Globalization;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Shared.DataTransferObjects;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerShopController : ControllerBase
    { 

        private readonly IDealerShopRepository _dealerShopRepository;
        private readonly CarDealershipContext _context;
        private readonly IMapper _mapper;
        public DealerShopController(CarDealershipContext context, IMapper mapper, IDealerShopRepository dealerShopRepository)
        {
            _context = context;
            _mapper = mapper;
            _dealerShopRepository = dealerShopRepository;
        }

        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DealerShop>))]
        [ProducesResponseType(400)]
        public IActionResult GetDealerShops()
        {
            var dealerShops = _dealerShopRepository.GetDealerShops();
            if (!ModelState.IsValid)
                return BadRequest();
            
            var dealerShopsDTO = _mapper.Map<List<DealerShopDTO>>(dealerShops);

            return Ok(dealerShopsDTO);
        }

        [HttpPost]
        public async Task<JsonResult> CreateDealerShopAsync([FromForm]DealerShopCreationDTO dealerShopDTO)
        {
            if (dealerShopDTO == null) return new JsonResult("Received data is null");
            var dealerShop = _mapper.Map<DealerShop>(dealerShopDTO);
            dealerShop.DealerShopId = Guid.NewGuid();

            dealerShop.Photos = new List<PhotoForDealerShop>();
            dealerShop.Cars = new List<Car>();
            dealerShop.Location = ConvertLocation(dealerShopDTO.Location);

            foreach(var file in dealerShopDTO.Files)
            {
                dealerShop.Photos.Add(ConvertFileToPhoto(file, dealerShop));
            }

            _context.DealerShops.Add(dealerShop);

            await _context.SaveChangesAsync();

            return new JsonResult("Dealer shop successfully created");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDealerShopAsync(Guid id)
        {
            var dealerShop = await _context.DealerShops.FirstOrDefaultAsync(shop => shop.DealerShopId == id);
            if (dealerShop == null)
            {
                return new JsonResult("Dealershop not found");
            }
            _context.DealerShops.Remove(dealerShop);
            await _context.SaveChangesAsync();
            return new JsonResult("Dealershop was successfully deleted");
        }


        [HttpPut]
        public async Task<IActionResult> EditDealerShopAsync([FromForm]DealerShopDTO dealerShopDTO) 
        {
            if (dealerShopDTO == null) return new JsonResult("Received data is null"); 
            var dealerShop = _mapper.Map<DealerShop>(dealerShopDTO);
            dealerShop.DealerShopId = Guid.Parse(dealerShopDTO.DealerShopId);
            
            dealerShop.Location = ConvertLocation(dealerShopDTO.Location);

            dealerShop.Cars = _context.Cars.Where(c => c.DealerShopId == dealerShop.DealerShopId).ToList();
            
            if (dealerShopDTO.Files != null)
            {
                var photos = new List<PhotoForDealerShop>();
                foreach (var file in dealerShopDTO.Files)
                {
                    photos.Add(ConvertFileToPhoto(file, dealerShop));
                }
                _context.PhotosForDealershop.RemoveRange(_context.PhotosForDealershop.Where(p => p.DealerShopId == dealerShop.DealerShopId));
                _context.PhotosForDealershop.AddRange(photos);
            }

            _context.DealerShops.Update(dealerShop);
            await _context.SaveChangesAsync();
            return new JsonResult("Dealershop was successfully updated");
        }



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
            return new Point(coordX, coordY) { SRID = 4326};
        }
    }
}
