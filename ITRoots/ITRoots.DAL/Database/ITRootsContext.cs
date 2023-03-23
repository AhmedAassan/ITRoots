using ITRoots.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.DAL.Database
{
    public class ITRootsContext : IdentityDbContext<ApplicationUser>
    {

        public ITRootsContext(DbContextOptions<ITRootsContext> opts) : base(opts)
        {

        }


        public DbSet<Product> Product { get; set; }
        public DbSet<invoices> invoices { get; set; }
        public DbSet<ProductInvoices> ProductInvoices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInvoices>().HasKey(a => new { a.ProductId, a.invoicesId });
            base.OnModelCreating(modelBuilder);
        }
      
    }
}
