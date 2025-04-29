

namespace Vendo.Domain.Entities
{
     public class Product:BaseNameableEntity
    {

        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Gender {  get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal Discount { get; set; }
      

        //Relational Properties
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }

    }
}

   
