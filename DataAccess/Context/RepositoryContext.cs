using Entities.Models;
using Entities.Models.Report;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<DealerRelationship> DealerRelationships { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PriceListDetail> PriceListDetails { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AdminOrderReport> AdminOrderReports { get; set; }
        public DbSet<DealerOrderReport> DealerOrderReports { get; set; }
        public DbSet<LowStockReport> LowStockReports { get; set; }
        public DbSet<ProductStockReport> ProductStockReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
