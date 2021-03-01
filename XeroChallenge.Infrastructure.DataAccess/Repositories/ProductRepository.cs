using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using XeroChallenge.Domain.Entities;
using XeroChallenge.Domain.Repositories;
using XeroChallenge.Infrastructure.DataAccess.DBContext;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace XeroChallenge.Infrastructure.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private DbContextOptionsBuilder<ProductsDbContext> _DbContextBuilder = new DbContextOptionsBuilder<ProductsDbContext>();
        public ProductRepository(IConfiguration  configuration)
        {
            _DbContextBuilder.UseSqlite(configuration.GetConnectionString("DatabaseConnection"));
        }
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Product Get(Guid Id)
        {
            using (var dbContext = new ProductsDbContext(_DbContextBuilder.Options))
            {
                return dbContext.Products.Include(p => p.Options).SingleOrDefault(p => p.Id == Id);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (var dbContext = new ProductsDbContext(_DbContextBuilder.Options))
            {
                return dbContext.Products.ToList();
            }
        }

        public Guid Save(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
