using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.Domain.Entities;


namespace PharmacySystem.Infrastructure.DbContext
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // بيزنس لوجيك: تحديد دقة الأسعار في قاعدة البيانات
            builder.Entity<Medicine>()
                .Property(m => m.CostPrice).HasColumnType("decimal(10,2)");

            builder.Entity<Medicine>()
                .Property(m => m.SellingPrice).HasColumnType("decimal(10,2)");

            builder.Entity<SaleInvoice>()
                .Property(s => s.TotalAmount).HasColumnType("decimal(10,2)");

            builder.Entity<SaleItem>()
                .Property(s => s.UnitPrice).HasColumnType("decimal(10,2)");

            // منع حذف القسم إذا كان يحتوي على أدوية (Business Rule)
            builder.Entity<Category>()
                .HasMany(c => c.Medicines)
                .WithOne(m => m.Category)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict Deleting
        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<SaleInvoice> SalesInvoices { get; set; }
        public DbSet<SaleItem> SalesItems { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
