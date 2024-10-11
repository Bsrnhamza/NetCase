using CaseWork.DataAccess.Context;
using CaseWork.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseWork.DataAccess.Repositories
{
    public class CarrierConfigurationRepository
    {
        private readonly AppDbContext _context;

        public CarrierConfigurationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCarrierConfigurationAsync(CarrierConfiguration configuration)
        {
            await _context.CarrierConfigurations.AddAsync(configuration);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CarrierConfiguration>> GetAllCarrierConfigurationsAsync()
        {
            return await _context.CarrierConfigurations.Include(c => c.Carrier).ToListAsync();
        }

        public async Task UpdateCarrierConfigurationAsync(CarrierConfiguration configuration)
        {
            _context.CarrierConfigurations.Update(configuration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarrierConfigurationAsync(int configId)
        {
            var config = await _context.CarrierConfigurations.FindAsync(configId);
            if (config != null)
            {
                _context.CarrierConfigurations.Remove(config);
                await _context.SaveChangesAsync();
            }
        }

        // Yeni Metot: Sipariş desisine göre en uygun kargo firmasını bul
        public async Task<CarrierConfiguration> GetCarrierByDesiAsync(int desi)
        {

            var suitableCarrier = await _context.CarrierConfigurations
                .Where(cc => cc.CarrierMinDesi <= desi && cc.CarrierMaxDesi >= desi)
                .OrderBy(cc => cc.CarrierCost) 
                .FirstOrDefaultAsync();

            if (suitableCarrier != null)
            {
                return suitableCarrier;
            }


            var closestCarrier = await _context.CarrierConfigurations
                .OrderBy(cc => Math.Abs(cc.CarrierMinDesi - desi)) 
                .FirstOrDefaultAsync();

            return closestCarrier;
        }

        public decimal CalculateCarrierCost(int desi, CarrierConfiguration carrierConfig)
        {
            if (carrierConfig.CarrierMinDesi <= desi && carrierConfig.CarrierMaxDesi >= desi)
            {
   
                return carrierConfig.CarrierCost;
            }
            else
            {
                int desiDifference = desi - carrierConfig.CarrierMaxDesi;
                return carrierConfig.CarrierCost + (desiDifference * carrierConfig.CostPerDesi);
            }
        }
    }
}
