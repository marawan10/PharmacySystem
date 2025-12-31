namespace PharmacySystem.Domain.Entities
{
    public class Medicine:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string GenericName { get; set; } = string.Empty; // الاسم العلمي

        public decimal CostPrice { get; set; }  // سعر التكلفة (للأدمن فقط)
        public decimal SellingPrice { get; set; } // سعر البيع (للجمهور)

        public int StockQuantity { get; set; } // الكمية المتاحة في المخزن
        public DateTime? ExpiryDate { get; set; } // تاريخ الانتهاء
        public bool IsDiscontinued { get; set; } // هل توقف إنتاجه؟

        // ربط بالقسم
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
    }
}
