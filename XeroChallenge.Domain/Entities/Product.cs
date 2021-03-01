using System;
using System.Collections.Generic;
using System.Linq;

namespace XeroChallenge.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public bool IsNew { get; }

        public List<ProductOption> Options { get; set; }

        public Product()
        {
            Id = Guid.NewGuid();
            IsNew = true;
        }

        public void RemoveProductOption(Guid productOptionId)
        {
            var option = Options?.SingleOrDefault(p => p.Id == productOptionId);
            Options?.Remove(option);
        }
    }
}
