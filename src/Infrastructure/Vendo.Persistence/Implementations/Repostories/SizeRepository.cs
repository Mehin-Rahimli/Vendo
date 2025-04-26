
using Vendo.Application.Abstractions.Repositories;
using Vendo.Domain.Entities;
using Vendo.Persistence.Contexts;
using Vendo.Persistence.Implementations.Repostories.Generic;

namespace Vendo.Persistence.Implementations.Repostories
{
    internal class SizeRepository : Repository<Size>, ISizeRepository
    {
        public SizeRepository(AppDbContext context) : base(context) { }
    }
}