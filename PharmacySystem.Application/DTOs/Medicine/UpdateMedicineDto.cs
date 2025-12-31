using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Application.DTOs.Medicine
{
    public class UpdateMedicineDto
    {
        public string Name { get; set; } = string.Empty;
        public string GenericName { get; set; } = string.Empty;

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public int StockQuantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsDiscontinued { get; set; }

        public int CategoryId { get; set; }
    }
}
