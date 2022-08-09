using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
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
    [Route("api/dealershops")]
    [ApiController]
    public class DealerShopController : ControllerBase
    {
        private readonly IServiceManager _service;
        public DealerShopController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDealerShops()
        {
            var dealerShops = await _service.DealerShopService.GetAllDealerShopsAsync(false);
            return Ok(dealerShops);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetDealerShop(Guid id)
        {
            var dealerShop = await _service.DealerShopService.GetDealerShopAsync(id, false);
            return Ok(dealerShop);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDealerShop([FromForm] DealerShopForCreationDTO dealerShopDTO)
        {
            if (dealerShopDTO is null)
                return BadRequest("DealerShopCreationDto is null");

            var dealerShopForReturn = await _service.DealerShopService.CreateDealerShopAsync(dealerShopDTO, dealerShopDTO.Files);

            return Ok(dealerShopForReturn);
        }


        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDealerShop(Guid id, [FromForm] DealerShopForUpdateDTO dealerShopForUpdateDTO)
        {
            if (dealerShopForUpdateDTO == null)
                return BadRequest("Received data is null");

            var dealerShopForReturn = await _service.DealerShopService.UpdateDealerShopAsync(id,
                dealerShopForUpdateDTO);

            return Ok(dealerShopForReturn);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDealerShop(Guid id)
        {
            await _service.DealerShopService.DeleteDealerShopAsync(id);

            return NoContent();
        }

        [HttpGet("ordinalnumbers")]
        public async Task<IActionResult> GetOrdinalNumbersForDealerShops()
        {
            var ordinalNumbersWithIds = await _service.DealerShopService.GetOrdinalNumbersAsync();
            return Ok(ordinalNumbersWithIds);
        } 
        
    }
}
