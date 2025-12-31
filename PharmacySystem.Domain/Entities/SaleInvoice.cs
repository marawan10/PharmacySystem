namespace PharmacySystem.Domain.Entities
{
    public class SaleInvoice:BaseEntity
    {
        public string InvoiceNumber { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string PharmacistId { get; set; } = string.Empty; // معرف الصيدلي الذي باع

        public virtual ICollection<SaleItem> SaleItems { get; set; } = new HashSet<SaleItem>();
    }
}
