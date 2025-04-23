using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            

        }
    }
}
