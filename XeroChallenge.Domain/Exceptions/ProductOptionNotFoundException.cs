﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XeroChallenge.Domain.Exceptions
{
    public class ProductOptionNotFoundException : DomainException
    {
        private const string PRODUCTOPTIONNOTFOUND = "The product option {0} wasn't found";
        public ProductOptionNotFoundException()
            : base(string.Format(PRODUCTOPTIONNOTFOUND, ""))
        {
        }

        public ProductOptionNotFoundException(string message)
            : base(message)
        {
        }

        public ProductOptionNotFoundException(Guid productId)
           : base(string.Format(PRODUCTOPTIONNOTFOUND, productId))
        {
        }

        public ProductOptionNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
