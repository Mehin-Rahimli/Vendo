

namespace Vendo.Domain.Entities
{
    public  class ProductImage:BaseNameableEntity
    {
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
      public bool IsPrimary { get; set; } = false;
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
