using CaseWork.DataAccess.Context;
using CaseWork.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseWork.DataAccess.Repositories
{
    public class CarrierRepository
    {
        private readonly AppDbContext _context;

        public CarrierRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCarrierAsync(Carrier carrier)
        {
            await _context.Carriers.AddAsync(carrier);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Carrier>> GetAllCarriersAsync()
        {
            return await _context.Carriers.ToListAsync();
        }

        public async Task UpdateCarrierAsync(Carrier carrier)
        {
            _context.Carriers.Update(carrier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarrierAsync(int carrierId)
        {
            var carrier = await _context.Carriers.FindAsync(carrierId);
            if (carrier != null)
            {
                _context.Carriers.Remove(carrier);
                await _context.SaveChangesAsync();
            }
        }
    }
}
