using Microsoft.EntityFrameworkCore;


namespace MvcTpv.Data
{
    //using System.Data.Entity;  obsolete  
    using MvcTpv.Models;

    public class MvcTpvContext : DbContext
    {
        public MvcTpvContext(DbContextOptions<MvcTpvContext> options) : base(options)
        {

        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<SaleMan> SaleMans { get; set; }
        public DbSet<Customer> Customers { get; set; }


        public DbSet<Sale> Sales { get; set; }

        public DbSet<Input> Inputs { get; set; }

        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<InputDetail> InputDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Categoria");
            modelBuilder.Entity<Product>().ToTable("Producto");
            modelBuilder.Entity<Provider>().ToTable("Provider");


            modelBuilder.Entity<SaleMan>().ToTable("SaleMan");
            modelBuilder.Entity<Customer>().ToTable("Customer");


            modelBuilder.Entity<Sale>().ToTable("Sale");
            modelBuilder.Entity<SaleDetail>().ToTable("SaleDetail");
            modelBuilder.Entity<Input>().ToTable("Input");
            modelBuilder.Entity<InputDetail>().ToTable("InputDetail");

            modelBuilder.Entity<Product>()
                .Property(u => u.Description)
                .HasColumnName("ProductDescription");
        }


     
    }
}