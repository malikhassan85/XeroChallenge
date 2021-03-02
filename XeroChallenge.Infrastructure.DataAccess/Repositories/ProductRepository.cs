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
        private DbContextOptionsBuilder<DatabaseContext> _DbContextBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        public ProductRepository(DatabaseContext dbContext, ILogger<ProductRepository> logger)
        {
            // Store the context
            this.Context = dbContext;

            // Initialise the DbSet
            this.Entities = this.Context.Set<Product>();

            _Logger = logger;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        protected DatabaseContext Context { get; }

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        protected DbSet<Product> Entities { get; }

        public async Task<Product> Get(Guid Id)
        {
            _Logger.LogInformation("Retrieving product: {id} from database", Id);
            return await Context.Products.Include(p => p.Options).SingleOrDefaultAsync(p => EF.Functions.Collate(p.Id, COLLATION) == Id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
                _Logger.LogInformation("Retrieving all products");
                return await Context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            _Logger.LogInformation("Retrieving products with name matches: {name}", name);
            return await Context.Products.Where(p => EF.Functions.Collate(p.Name, COLLATION) == name).ToListAsync();
        }

        public async Task<Guid> Save(Product product)
        {
            if (product is null) throw new ArgumentNullException(nameof(product));

            if (product.IsNew)
            {
                _Logger.LogInformation("Creating a new product: {@product}", product);
                Context.Products.Add(product);
            }
            else
            {
                var existingProduct = await Context.Products.Include(p => p.Options).SingleOrDefaultAsync(p => EF.Functions.Collate(p.Id, COLLATION) == product.Id);
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
                            Context.Entry(existingOption).CurrentValues.SetValues(option);
                        }
                    }

                    var deletedOptions = existingProduct.Options.FindAll(p => !product.Options.Any(x => x.Id == p.Id));
                    existingProduct.Options.RemoveAll(p => deletedOptions.Any(x => x.Id == p.Id));
                }
            }

            await Context.SaveChangesAsync();
            return product.Id;
        }

        public async Task Delete(Guid Id)
        {
            _Logger.LogInformation("Getting product: {Id} to be deleted", Id);
            var product = Context.Products.Include(p => p.Options).SingleOrDefault(p => EF.Functions.Collate(p.Id, COLLATION) == Id);
            if (product != null)
            {
                _Logger.LogInformation("Deleting product: {Id}", Id);
                Context.Products.Remove(product);
                await Context.SaveChangesAsync();
            }
            else
            {
                _Logger.LogWarning("Deleting a product which is already deleted or non-existed: {Id}", Id);
                throw new ProductIsNotAvailable("The product was already deleted or it doesn't exist");
            }
        }
    }
}
