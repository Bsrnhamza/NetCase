using CaseWork.DataAccess.Repositories;
using CaseWork.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierConfigurationController : ControllerBase
    {
        private readonly CarrierConfigurationRepository _carrierConfigRepository;

        public CarrierConfigurationController(CarrierConfigurationRepository carrierConfigRepository)
        {
            _carrierConfigRepository = carrierConfigRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCarrierConfigurations()
        {
            var configurations = await _carrierConfigRepository.GetAllCarrierConfigurationsAsync();
            return Ok(configurations);
        }
        [HttpPost]
        public async Task<IActionResult> AddCarrierConfiguration(CarrierConfiguration configuration)
        {
            await _carrierConfigRepository.AddCarrierConfigurationAsync(configuration);
            return Ok("Kargo firması konfigürasyonu başarıyla eklendi!");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCarrierConfiguration(CarrierConfiguration configuration)
        {
            await _carrierConfigRepository.UpdateCarrierConfigurationAsync(configuration);
            return Ok("Kargo firması konfigürasyonu başarıyla güncellendi!");
        }
        [HttpDelete("{configId}")]
        public async Task<IActionResult> DeleteCarrierConfiguration(int configId)
        {
            await _carrierConfigRepository.DeleteCarrierConfigurationAsync(configId);
            return Ok("Kargo firması konfigürasyonu başarıyla silindi!");
        }
    }
}
