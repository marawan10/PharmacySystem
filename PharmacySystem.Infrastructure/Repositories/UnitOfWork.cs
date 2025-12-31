using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using PharmacySystem.Application;
using PharmacySystem.Application.Interfaces.Repositories;
using PharmacySystem.Domain.Entities;
using PharmacySystem.Infrastructure.DbContext;

namespace PharmacySystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Medicine> Medicines { get;private set;  }

        public IGenericRepository<Category> Catigroies { get; private set; }

        public IGenericRepository<SaleInvoice> SaleInvoices { get; private set; }

        public IGenericRepository<SaleItem> SaleItems { get; private set; }

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext _context )
        {
            this._context = _context;
            Medicines = new GenericRepository<Medicine>(_context);
            Catigroies = new GenericRepository<Category>(_context);
            SaleInvoices = new GenericRepository<SaleInvoice>(_context);
            SaleItems = new GenericRepository<SaleItem>(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();

    }
}
