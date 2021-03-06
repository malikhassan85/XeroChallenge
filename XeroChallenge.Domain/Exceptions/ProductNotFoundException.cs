﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XeroChallenge.Domain.Exceptions
{
    public class ProductNotFoundException : DomainException
    {
        private const string PRODUCTNOTFOUND = "The product {0} wasn't found";
        public ProductNotFoundException()
            : base(string.Format(PRODUCTNOTFOUND,""))
        {
        }

        public ProductNotFoundException(string message)
            : base(message)
        {
        }

        public ProductNotFoundException(Guid productId)
           : base(string.Format(PRODUCTNOTFOUND, productId))
        {
        }

        public ProductNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
