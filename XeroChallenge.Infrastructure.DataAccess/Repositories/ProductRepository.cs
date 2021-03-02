using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using XeroChallenge.Domain.Entities;
using XeroChallenge.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using XeroChallenge.Infrastructure.Persistence.DBContext;
using XeroChallenge.Infrastructure.Persistence.Exceptions;

namespace XeroChallenge.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private string COLLATION = "nocase";

        private ILogger<ProductRepository> _Logger;
        private DbContextOptionsBuilder<ProductsDbContext> _DbContextBuilder = new DbContextOptionsBuilder<ProductsDbContext>();
        public ProductRepository(IConfiguration  configuration, ILogger<ProductRepository> logger)
        {
            _DbContextBuilder.UseSqlite(configuration.GetConnectionString("DatabaseConnection"));
            _Logger = logger;
        }
       
        public async Task<Product> Get(Guid Id)
        {
            using (var dbContext = new ProductsDbContext(_DbContextBuilder.Options))
            {
                _Logger.LogInformation("Retrieving product: {id} from database", Id);
                return await dbContext.Products.Include(p => p.Options).SingleOrDefaultAsync(p => EF.Functions.Collate(p.Id, COLLATION) == Id);
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            using (var dbContext = new ProductsDbContext(_DbContextBuilder.Options))
            {
                _Logger.LogInformation("Retrieving all products");
                return await dbContext.Products.ToListAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllByName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            using (var dbContext = new ProductsDbContext(_DbContextBuilder.Options))
            {
                _Logger.LogInformation("Retrieving products with name matches: {name}", name);
                return await dbContext.Products.Where(p => EF.Functions.Collate(p.Name, COLLATION) == name).ToListAsync();
            }
        }

        public async Task<Guid> Save(Product product)
        {
            if (product is null) throw new ArgumentNullException(nameof(product));

            using (var dbContext = new ProductsDbContext(_DbContextBuilder.Options))
            {
                if(product.IsNew)
                {
                    _Logger.LogInformation("Creating a new product: {@product}", product);
                    dbContext.Products.Add(product);
                }
                else
                {
                    var existingProduct = await dbContext.Products.Include(p => p.Options).SingleOrDefaultAsync(p => EF.Functions.Collate(p.Id, COLLATION) == product.Id);
                    if (existingProduct == null)
                    {
                        _Logger.LogError("Updating a product that doesnt exists: {Id}", product.Id);
                        throw new ProductIsNotAvailable("Updating a product that doesnt exists");
                    }
                    _Logger.LogInformation("Updating product: {Id} ", product.Id);

                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.DeliveryPrice = product.DeliveryPrice;
                    existingProduct.Price = product.Price;

                    if (product.Options != null)
                    {
                        foreach (var option in product.Options)
                        {
                            var existingOption = existingProduct.Options
                                .AsQueryable()
                                .FirstOrDefault(p => p.Id == option.Id);

                            if (existingOption == null)
                            {
                                existingProduct.Options.Add(option);
                            }
                            else
                            {
                                dbContext.Entry(existingOption).CurrentValues.SetValues(option);
                            }
                        }

                        var deletedOptions = existingProduct.Options.FindAll(p => !product.Options.Any(x => x.Id == p.Id));
                        existingProduct.Options.RemoveAll(p => deletedOptions.Any(x => x.Id == p.Id));
                    }
                }

                await dbContext.SaveChangesAsync();
                return product.Id;
            }
        }

        public async Task Delete(Guid Id)
        {
            using (var dbContext = new ProductsDbContext(_DbContextBuilder.Options))
            {
                _Logger.LogInformation("Getting product: {Id} to be deleted", Id);
                var product = dbContext.Products.Include(p => p.Options).SingleOrDefault(p => EF.Functions.Collate(p.Id, COLLATION) == Id);
                if (product != null)
                {
                    _Logger.LogInformation("Deleting product: {Id}", Id);
                    dbContext.Products.Remove(product);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    _Logger.LogWarning("Deleting a product which is already deleted or non-existed: {Id}", Id);
                    throw new ProductIsNotAvailable("The product was already deleted or it doesn't exist");
                }
            }
        }
    }
}
