using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XeroChallenge.WebApi.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Required]
        public decimal? DeliveryPrice { get; set; }
    }
}
