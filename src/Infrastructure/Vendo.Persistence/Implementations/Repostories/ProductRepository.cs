using Microsoft.EntityFrameworkCore;
using Vendo.Application.Abstractions.Repositories;
using Vendo.Domain.Entities;
using Vendo.Persistence.Contexts;
using Vendo.Persistence.Implementations.Repostories.Generic;


namespace Vendo.Persistence.Implementations.Repostories
{
    internal class ProductRepository:Repository<Product>,IProductRepository
    {
        public ProductRepository(AppDbContext context):base(context) { }

        public async Task<IEnumerable<T>> GetManyToManyEntities<T>(ICollection<int> ids) where T : BaseEntity
        {
            return await _context.Set<T>()
           .Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}
