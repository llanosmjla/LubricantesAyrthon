using Microsoft.EntityFrameworkCore;
using LubricantesAyrthonAPI.Models;

namespace LubricantesAyrthonAPI.Configuration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Seller> Sellers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Sale -> Customer (many-to-one) Muchas ventas pueden pertenecer a un cliente
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict);

            // Sale -> Seller (many-to-one) Muchas ventas pueden pertenecer a un vendedor
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Seller)
                .WithMany(se => se.Sales)
                .HasForeignKey(s => s.IdSeller)
                .OnDelete(DeleteBehavior.Restrict);

            // SaleDetail -> Sale (many-to-one) Muchos detalles de venta pueden pertenecer a una venta
            modelBuilder.Entity<SaleDetail>()
                .HasOne(sd => sd.Sale)
                .WithMany(s => s.SaleDetails)
                .HasForeignKey(sd => sd.IdSale)
                .OnDelete(DeleteBehavior.Cascade);

            // SaleDetail -> Product (many-to-one) Muchos detalles de venta pueden pertenecer a un producto
            modelBuilder.Entity<SaleDetail>()
                .HasOne(sd => sd.Product)
                .WithMany(p => p.SaleDetails)
                .HasForeignKey(sd => sd.IdProduct)
                .OnDelete(DeleteBehavior.Restrict);

            // Ci unique constraint
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Ci)
                .IsUnique();

        }
    }
}