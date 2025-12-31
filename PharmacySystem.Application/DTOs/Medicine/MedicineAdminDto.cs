using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Application.DTOs.Medicine
{
    public class MedicineAdminDto
    {
        public string Name { get; set; }
        public decimal CostPrice { get; set; }  // سعر التكلفة (للأدمن فقط)
        public decimal SellingPrice { get; set; } // سعر البيع (للجمهور)
        public int Quantity { get; set; }
    }
}
