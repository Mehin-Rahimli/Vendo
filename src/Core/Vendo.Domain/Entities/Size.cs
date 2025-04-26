

namespace Vendo.Domain.Entities
{
    public class Size : BaseNameableEntity
    {
        public ICollection<ProductSize> ProductSizes { get; set; }
    }
}
