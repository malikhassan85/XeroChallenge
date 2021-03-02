using System.Collections.Generic;
using System.Threading.Tasks;
using XeroChallenge.Domain.Entities;

namespace XeroChallenge.Domain.Repositories
{
    /// <summary>
    /// Represents the interface for the product entity repository
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllByName(string name);
    }
}
