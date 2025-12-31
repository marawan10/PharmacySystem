using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Domain.Entities
{
    public class SaleItem:BaseEntity
    {
        public int MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // السعر وقت البيع (لأنه قد يتغير مستقبلاً)

        public int SaleInvoiceId { get; set; }
        public virtual SaleInvoice SaleInvoice { get; set; } = null!;
    }
}
