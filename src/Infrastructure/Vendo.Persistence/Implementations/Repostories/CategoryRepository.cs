using Vendo.Application.Abstractions.Repositories;
using Vendo.Domain.Entities;
using Vendo.Persistence.Contexts;
using Vendo.Persistence.Implementations.Repostories.Generic;


namespace Vendo.Persistence.Implementations.Repostories
{
    internal class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context):base(context) { }
    }
}
