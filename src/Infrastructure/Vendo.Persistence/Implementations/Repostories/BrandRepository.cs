
using Vendo.Application.Abstractions.Repositories;
using Vendo.Domain.Entities;
using Vendo.Persistence.Contexts;
using Vendo.Persistence.Implementations.Repostories.Generic;

namespace Vendo.Persistence.Implementations.Repostories
{
    internal class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context) { }
    }
}
