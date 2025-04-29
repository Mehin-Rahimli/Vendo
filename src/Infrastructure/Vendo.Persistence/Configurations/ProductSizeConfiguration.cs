
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vendo.Domain.Entities;

namespace Vendo.Persistence.Configurations
{
    internal class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
    {
        public void Configure(EntityTypeBuilder<ProductSize> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.SizeId });
        }
    }
}
