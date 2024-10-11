using CaseWork.DataAccess.Repositories;
using CaseWork.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly CarrierRepository _carrierRepository;

        public CarrierController(CarrierRepository carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarriers()
        {
            var carriers = await _carrierRepository.GetAllCarriersAsync();
            return Ok(carriers);
        }

        [HttpPost]
        public async Task<IActionResult> AddCarrier(Carrier carrier)
        {
            await _carrierRepository.AddCarrierAsync(carrier);
            return Ok("Kargo firması başarıyla eklendi!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarrier(Carrier carrier)
        {
            await _carrierRepository.UpdateCarrierAsync(carrier);
            return Ok("Kargo firması başarıyla güncellendi!");
        }

        [HttpDelete("{carrierId}")]
        public async Task<IActionResult> DeleteCarrier(int carrierId)
        {
            await _carrierRepository.DeleteCarrierAsync(carrierId);
            return Ok("Kargo firması başarıyla silindi!");
        }
    }
}
