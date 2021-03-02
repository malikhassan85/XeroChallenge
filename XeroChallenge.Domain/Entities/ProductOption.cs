using System;
namespace XeroChallenge.Domain.Entities
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Product Product { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsNew => Id == Guid.Empty;

        public ProductOption()
        {
        }

        public void UpdateValues(ProductOption productOption)
        {
            Name = productOption.Name;
            Description = productOption.Description;
        }
    }
}
