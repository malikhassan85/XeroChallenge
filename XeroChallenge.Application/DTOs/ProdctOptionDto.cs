using System;
using System.Collections.Generic;
using System.Text;

namespace XeroChallenge.Application.DTOs
{
    public class ProductOptionDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
