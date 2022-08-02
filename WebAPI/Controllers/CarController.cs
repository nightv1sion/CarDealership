using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Shared.DataTransferObjects;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IDealerShopRepository _dealerShopRepository;
        private readonly IMapper _mapper;

        public CarController(ICarRepository carRepository, IMapper mapper, IDealerShopRepository dealerShopRepository)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _dealerShopRepository = dealerShopRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetCars()
        {
            var cars =  _mapper.Map<List<CarDTO>>(_carRepository.GetCars());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cars);
        }
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult CreateCar([FromForm] CarCreationDTO carCreationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dealerShopId = Guid.Parse(carCreationDTO.DealerShopId);
            if (!_dealerShopRepository.DealerShopExists(dealerShopId))
                return NotFound("Such dealershop is not found");

            var car = _mapper.Map<Car>(carCreationDTO);
            car.DealerShopId = dealerShopId;
            car.DealerShop = _dealerShopRepository.GetDealerShop(dealerShopId);
            car.Photos = new List<PhotoForCar>();

            _carRepository.Add(car);

            _carRepository.SaveAsync();

            return Ok("Car was successfully created");
        }
    }
}
