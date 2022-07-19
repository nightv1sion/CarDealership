using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerShopController : ControllerBase
    {
        private readonly CarDealershipContext _context;
        public DealerShopController(CarDealershipContext context)
        {
            _context = context;
        }

        [HttpGet("dealershops")]
        public JsonResult GetDealerShops()
        {
            var dealershops = _context.DealerShops.ToList();
            return new JsonResult(dealershops);
        }
        [HttpPost("dealershops")]
        public JsonResult CreateDealerShop()
        {
            
        }
    }
}
