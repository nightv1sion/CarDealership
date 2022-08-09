﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Presentation.ActionFilters;

namespace WebAPI.Presentation.Contollers
{
    [ApiController]
    [Route("api/dealershops/{dealerShopId:guid}/cars")]
    public class CarController : ControllerBase
    {
        private readonly IServiceManager _service;
        
        public CarController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarsForDealerShop(Guid dealerShopId)
        {
            var cars = await _service.CarService.GetCarsForDealerShopAsync(dealerShopId, false);
            return Ok(cars);
        }

        [HttpGet("{id:guid}", Name = "GetCarForDealerShopById")]
        public async Task<IActionResult> GetCarForDealerShopById(Guid dealerShopId, Guid id)
        {
            var carForReturn = await _service.CarService.GetCarForDealerShopByIdAsync(dealerShopId, id, false);
            return Ok(carForReturn);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateForDealerShopById(Guid dealerShopId, [FromBody]CarForCreationDTO car)
        {
            var carForReturn = await _service.CarService.CreateCarAsync(dealerShopId, car);
            return CreatedAtRoute("GetCarForDealerShopById", new { dealerShopId = dealerShopId, id = carForReturn.CarId }, carForReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarForDealerShop(Guid dealerShopId, Guid id)
        {
            await _service.CarService.DeleteCarForDealerShop(dealerShopId, id, false);
            return NoContent();
        }
    }
}