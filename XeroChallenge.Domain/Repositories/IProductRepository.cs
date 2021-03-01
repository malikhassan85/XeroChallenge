using System;
using System.Collections.Generic;
using System.Text;
using XeroChallenge.Domain.Entities;

namespace XeroChallenge.Domain.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product Get(Guid Id);

        void Delete(Guid Id);

        Guid Save(Product product);
    }
}
