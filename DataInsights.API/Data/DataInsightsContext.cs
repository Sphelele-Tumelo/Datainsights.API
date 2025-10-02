using DataInsights.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DataInsights.API.Data
{
    public class SQLDatabase : DbContext
    {

        public SQLDatabase(DbContextOptions<SQLDatabase> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<CustomerDTO> Customers { get; set; } = null!;

        public DbSet<Sale> Sales { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(s => s.Product_ID);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
                

            modelBuilder.Entity<CustomerDTO>()
                .HasKey(s => s._customerID);

            modelBuilder.Entity<Sale>()
                .HasKey(s => s.Sales_ID);
            modelBuilder.Entity<Sale>()
                .Property(s => s.Total_Amount)
                .HasPrecision(18, 2);
        }
    }


    public class PostgreSQLDb : DbContext
    {
        public PostgreSQLDb(DbContextOptions<PostgreSQLDb> options):base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerDTO> Customers { get; set; }
        public DbSet <Sale> Sales { get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(s => s.Product_ID);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
           /* modelBuilder.Entity<Product>()
               .HasOne(c => c.Product_ID)
               .WithMany(p => p.Product)
               .HasForeignKey(c => c.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
           */

            modelBuilder.Entity<CustomerDTO>()
                .HasKey(s => s._customerID);

            modelBuilder.Entity<Sale>()
                .HasKey(s => s.Sales_ID);
            modelBuilder.Entity<Sale>()
                .Property(s => s.Total_Amount)
                .HasPrecision(18, 2);
        }
    }
}
