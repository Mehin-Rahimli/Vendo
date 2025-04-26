using Vendo.Domain.Entities;


namespace Vendo.Application.Abstractions.Repositories
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<IEnumerable<T>> GetManyToManyEntities<T>
          (ICollection<int> ids) where T : BaseEntity;
    }
}
