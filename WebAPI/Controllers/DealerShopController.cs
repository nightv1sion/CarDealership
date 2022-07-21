using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var dealershops = _context.DealerShops.ToList();
            var dealershopsDTO = _mapper.Map<List<DealerShopDTO>>(dealershops);
            if (dealershopsDTO == null) return new JsonResult("Received data is null");
            return new JsonResult(dealershopsDTO);
        }

        [HttpPost]
        public async Task<JsonResult> CreateDealerShopAsync(DealerShopCreationDTO dealershopDTO)
        {
            if (dealershopDTO == null) return new JsonResult("Received data is null");
            var dealershop = _mapper.Map<DealerShop>(dealershopDTO);

            _context.DealerShops.Add(dealershop);
            await _context.SaveChangesAsync();

            return new JsonResult("Dealer shop successfully created");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDealerShopAsync(Guid id)
        {
            var dealershop = await _context.DealerShops.FirstOrDefaultAsync(shop => shop.DealerShopId == id);
            if(dealershop == null)
            {
                return new JsonResult ("Dealershop not found");
            }
            _context.DealerShops.Remove(dealershop);
            await _context.SaveChangesAsync();
            return new JsonResult("Dealershop was successfully deleted");
        }
        
    }
}
