
using Microsoft.EntityFrameworkCore;
using PharmacySystem.Application.Interfaces.Repositories;
using PharmacySystem.Domain.Entities;
using PharmacySystem.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Infrastructure.Repositories
{
    public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<string>> GetAllNamesAsync()
        {
            return await _context.Medicines
                .Select(m => m.Name)
                .ToListAsync();
        }
    }

}
