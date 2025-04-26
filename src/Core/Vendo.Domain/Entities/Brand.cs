
namespace Vendo.Domain.Entities
{
    public class Brand : BaseNameableEntity
    {

        public ICollection <Product> Products   {get;set;}
    }
}
