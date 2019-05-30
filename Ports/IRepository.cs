using System;
using System.Threading.Tasks;
using Common;

namespace Ports
{
    public interface IRepository
    {
        /// <summary>
        /// Creates an aggregate by calling <see cref="aggregateCreator"/> and inserting created aggregate to the repository.
        /// </summary>
        /// <param name="aggregateCreator">Generic creator function which builds an aggregate.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns success if aggregate has been created and inserted to the repository; otherwise, if aggregate already exists, returns failure.</returns>
        Task<Result> Create<T>(Func<T> aggregateCreator) where T : AggregateRoot, new();
        
        /// <summary>
        /// Retrieves existing aggregate from the repository and executes the <see cref="aggregateTransformer"/> in
        /// order to pass some command to the aggregate. As a result of <see cref="aggregateTransformer"/> execution
        /// all changes of the aggregate will be persisted in the database.
        /// </summary>
        /// <param name="aggregateId">Unique ID of an aggregate.</param>
        /// <param name="aggregateTransformer">Generic transformer function that will be called if aggregate exists.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns success if aggregate has been found and transformer executed without errors.</returns>
        Task<Result> BorrowBy<T>(AggregateId aggregateId, Func<T, Result<T>> aggregateTransformer) where T : AggregateRoot, new();
    }
}