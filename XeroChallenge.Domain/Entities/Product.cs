using System;
using System.Collections.Generic;
using System.Linq;

namespace XeroChallenge.Domain.Entities
{
    public class Product : IAggregateRoot
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public bool IsNew => Id == Guid.Empty;

        public List<ProductOption> Options { get; set; }

        public void RemoveProductOption(Guid productOptionId)
        {
            if (Options == null)
                return;

            var option = Options.SingleOrDefault(p => p.Id == productOptionId);
            Options.Remove(option);
        }

        public void AddProductOption(ProductOption productOption)
        {
            if (Options == null)
                Options = new List<ProductOption>();

            productOption.Product = this;
            Options.Add(productOption);
        }
    }
}
