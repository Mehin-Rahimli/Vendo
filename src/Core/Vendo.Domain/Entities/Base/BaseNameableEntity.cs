

namespace Vendo.Domain.Entities
{
    public abstract class BaseNameableEntity:BaseEntity
    {
        public string Name { get; set; }
    }
}
