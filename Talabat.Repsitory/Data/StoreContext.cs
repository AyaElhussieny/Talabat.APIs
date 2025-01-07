using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Repsitory.Data.Cofigruations;

namespace Talabat.Repsitory.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) 
        { 

        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent APIs Configurations

            //modelBuilder.ApplyConfiguration(new ProductConfigurations()); // ProductConfigurations old solution

            // any configurations in the assembly automatically 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // new solution

            base.OnModelCreating(modelBuilder);

        }

        /// representations Tables
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
    }
}
