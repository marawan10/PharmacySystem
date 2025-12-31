using PharmacySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Application.Interfaces.Repositories
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        Task<IEnumerable<string>> GetAllNamesAsync();
    }
}
