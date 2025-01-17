using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProiectPSSC_order_placement.DB
{
    public class ShopDbContext : DbContext
    {
        public DbSet<ProductEntity> ProductEntities { get; set; }
        public DbSet<CustomerEntity> CustomerEntities { get; set; }
        public DbSet<OrderEntity> OrderEntities { get; set; }
        public DbSet<OrderItemEntity> OrderItemEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:psscserverlidia.database.windows.net,1433;Initial Catalog=database;Persist Security Info=False;User ID=ligia1002;Password=Angi2468;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.ToTable("Products"); // Maparea la tabela Products

                entity.Property(e => e.ProductId).HasColumnName("Id"); // Mapare pentru ID
                entity.Property(e => e.ProductCode).HasColumnName("Code"); // Mapare pentru cod
                entity.Property(e => e.ProductName).HasColumnName("Name"); // Mapare pentru nume
                entity.Property(e => e.Price).HasColumnName("Price"); // Mapare pentru preț
                entity.Property(e => e.QuantityType).HasColumnName("QuantityType"); // Mapare pentru tip cantitate
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.ToTable("Customers");
                entity.Property(e => e.CustomerId).HasColumnName("Id");
                entity.Property(e => e.CustomerCode).HasColumnName("Code");
                entity.Property(e => e.CustomerName).HasColumnName("Name");
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("Orders");
                entity.Property(e => e.OrderId).HasColumnName("Id");
                entity.Property(e => e.DeliveryAddress).HasColumnName("DeliveryAddress");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");
            });

            modelBuilder.Entity<OrderItemEntity>(entity =>
            {
                entity.ToTable("OrderItem");
                entity.Property(e => e.OrderItemId).HasColumnName("Id");
                entity.Property(e => e.OrderId).HasColumnName("OrderId");
                entity.Property(e => e.ProductId).HasColumnName("ProductId");
                entity.Property(e => e.Quantity).HasColumnName("Quantity");
            });

            base.OnModelCreating(modelBuilder);
        }

    }

    public class ProductEntity
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string QuantityType { get; set; }
    }

    public class CustomerEntity
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
    }

    public class OrderEntity
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
    }

    public class OrderItemEntity
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public int Quantity { get; set; }
    }
}
