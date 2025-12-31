namespace PharmacySystem.Domain.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // علاقة: التصنيف الواحد يحتوي على أدوية كثيرة
        public virtual ICollection<Medicine> Medicines { get; set; } = new HashSet<Medicine>();
    }
}
