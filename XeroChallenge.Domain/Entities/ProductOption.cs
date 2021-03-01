using System;
using System.Collections.Generic;
using System.Text;

namespace XeroChallenge.Domain.Entities
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Product Product { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsNew { get; }

        public ProductOption()
        {
            Id = Guid.NewGuid();
            IsNew = true;
        }
    }
}
