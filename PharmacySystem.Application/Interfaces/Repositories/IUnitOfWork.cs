using System;
using System.Collections.Generic;
using System.Text;
using PharmacySystem.Domain;
using PharmacySystem.Domain.Entities;

namespace PharmacySystem.Application.Interfaces.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Medicine> Medicines { get; }
        IGenericRepository<Category> Catigroies { get; }
        IGenericRepository<SaleInvoice> SaleInvoices { get; }
        IGenericRepository<SaleItem> SaleItems { get; }
        Task<int> CompleteAsync();
    }
}
