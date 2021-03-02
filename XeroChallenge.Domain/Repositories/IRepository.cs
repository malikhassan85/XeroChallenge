using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XeroChallenge.Domain.Entities;

namespace XeroChallenge.Domain.Repositories
{
    /// <summary>
    /// Marker Interface for all the repositories in the system
    /// </summary>
    public interface IRepository
    {
    }

    /// <summary>
    /// Generic interface for all the repositories in the system
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>: IRepository
        where T: IAggregateRoot
    {
        /// <summary>
        /// Get Items based on a
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid Id);

        Task Delete(Guid Id);

        Task<Guid> Save(T product);
    }
}
