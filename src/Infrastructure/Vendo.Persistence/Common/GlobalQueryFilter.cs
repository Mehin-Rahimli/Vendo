using Azure;
using Microsoft.EntityFrameworkCore;
using Vendo.Domain.Entities;

namespace Vendo.Persistence.Common
{
    internal static class GlobalQueryFilter
    {
        public static void ApplyFilter<T>(this ModelBuilder modelBuilder) where T : BaseEntity, new()
        {
            modelBuilder.Entity<T>().HasQueryFilter(c => c.IsDeleted == false);
        }

        
             public static void ApplyQueryFilters(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyFilter<Category>();
            modelBuilder.ApplyFilter<Product>();
            modelBuilder.ApplyFilter<Size>();
            modelBuilder.ApplyFilter<Brand>();
            modelBuilder.ApplyFilter<Color>();
            modelBuilder.ApplyFilter<Category>();

        }

    }
    }

