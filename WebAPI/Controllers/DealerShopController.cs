using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using WebAPI.Models;
using WebAPI.Shared.DataTransferObjects;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerShopController : ControllerBase
    {
        private readonly CarDealershipContext _context;
        private readonly IMapper _mapper;
        public DealerShopController(CarDealershipContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public JsonResult GetDealerShops()
        {
            var dealerShops = _context.DealerShops.ToList();
            if (dealerShops.Count == 0) return new JsonResult("There are no dealershops");
            var dealerShopsDTO = _mapper.Map<List<DealerShopDTO>>(dealerShops);
            return new JsonResult(dealerShopsDTO);
        }

        [HttpPost]
        public async Task<JsonResult> CreateDealerShopAsync([FromForm]DealerShopCreationDTO dealerShopDTO)
        {
            if (dealerShopDTO == null) return new JsonResult("Received data is null");
            var dealerShop = _mapper.Map<DealerShop>(dealerShopDTO);
            dealerShop.DealerShopId = Guid.NewGuid();

            dealerShop.Photos = new List<PhotoForDealerShop>();
            dealerShop.Cars = new List<Car>();
            dealerShop.Location = new Point(13.003725d, 55.604870d) { SRID = 4326};

            foreach(var file in dealerShopDTO.Files)
            {
                var photo = new PhotoForDealerShop()
                {
                    PhotoId = Guid.NewGuid(),
                    Description = file.FileName,
                    Size = file.Length,
                    DealerShop = dealerShop, 
                    DealerShopId = dealerShop.DealerShopId,
                };
                if(file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        photo.Bytes = fileBytes;
                    }

                }
                dealerShop.Photos.Add(photo);
            }

            _context.DealerShops.Add(dealerShop);

            try
            {
                await _context.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


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
        public async Task<IActionResult> EditDealerShopAsync(DealerShopDTO dealerShopDTO) 
        {
            if (dealerShopDTO == null) return new JsonResult("Received data is null"); 
            var dealerShop = _mapper.Map<DealerShop>(dealerShopDTO);

            _context.DealerShops.Update(dealerShop);
            await _context.SaveChangesAsync();
            return new JsonResult("Dealershop was successfully updated");
        }
    }
}
