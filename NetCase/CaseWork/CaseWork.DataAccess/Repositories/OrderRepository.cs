using CaseWork.DataAccess.Context;
using CaseWork.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseWork.DataAccess.Repositories
{
    public class OrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
            var bestCarrierConfig = await FindBestCarrierForOrderAsync(order.OrderDesi);

            if (bestCarrierConfig != null)
            {
                var shippingCost = CalculateShippingCost(order.OrderDesi, bestCarrierConfig);

                order.CarrierID = bestCarrierConfig.CarrierID;
                order.OrderCarrierCost = shippingCost;
                order.OrderDate = DateTime.Now;
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var bestCarrierConfig = await FindBestCarrierForOrderAsync(order.OrderDesi);

            if (bestCarrierConfig != null)
            {
                var shippingCost = CalculateShippingCost(order.OrderDesi, bestCarrierConfig);
                order.OrderCarrierCost = shippingCost;
                order.CarrierID = bestCarrierConfig.CarrierID;
            }

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<CarrierConfiguration> FindBestCarrierForOrderAsync(int orderDesi)
        {
            var suitableCarrier = await _context.CarrierConfigurations
                .Include(c => c.Carrier)
                .Where(c => c.CarrierMinDesi <= orderDesi && c.CarrierMaxDesi >= orderDesi)
                .OrderBy(c => c.CarrierCost) 
                .FirstOrDefaultAsync();

            if (suitableCarrier != null)
            {
                return suitableCarrier;
            }

            return await _context.CarrierConfigurations
                .Include(c => c.Carrier)
                .OrderBy(c => Math.Abs(c.CarrierMaxDesi - orderDesi))
                .FirstOrDefaultAsync();
        }

        public decimal CalculateShippingCost(int orderDesi, CarrierConfiguration carrierConfig)
        {
            if (carrierConfig.CarrierMinDesi <= orderDesi && carrierConfig.CarrierMaxDesi >= orderDesi)
            {
                return carrierConfig.CarrierCost;
            }

            var desiDifference = orderDesi - carrierConfig.CarrierMaxDesi;

            if (desiDifference > 0)
            {
                return carrierConfig.CarrierCost + (desiDifference * carrierConfig.CostPerDesi);
            }

            return carrierConfig.CarrierCost;
        }
    }
}
