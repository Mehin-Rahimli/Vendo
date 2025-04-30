using Azure;
using System.Reflection.Metadata;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Vendo.Domain.Entities;
using Vendo.Persistence.Common;

namespace Vendo.Persistence.Contexts
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductColor> ProductSizes { get; set; }
        public DbSet<Brand> Brands { get; set; }
       public DbSet<ProductImage> ProductImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyQueryFilters();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var data = ChangeTracker.Entries<BaseEntity>();
            foreach (var item in data)
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.Entity.ModifiedAt = DateTime.Now;
                        break;
                    case EntityState.Added:
                        item.Entity.CreatedAt = DateTime.Now;
                        item.Entity.CreatedBy = "admin";
                        item.Entity.ModifiedAt = DateTime.Now;
                        break;


                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
