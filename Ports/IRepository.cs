using System;
using System.Threading.Tasks;
using Common;

namespace Ports
{
    public interface IRepository
    {
        /// <summary>
        /// Inserts an aggregate to the repository.
        /// </summary>
        /// <param name="newAggregate">New aggregate to add to the repository.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns success if aggregate has been inserted to the repository; otherwise, if aggregate already exists, returns failure.</returns>
        Task<Result> InsertNew<T>(T newAggregate) where T : AggregateRoot, new();
        
        /// <summary>
        /// Retrieves existing aggregate from the repository and executes the <see cref="aggregateTransformer"/> in
        /// order to pass some command to the aggregate. As a result of <see cref="aggregateTransformer"/> execution
        /// all changes of the aggregate will be persisted in the database.
        /// </summary>
        /// <param name="aggregateId">Unique ID of an aggregate.</param>
        /// <param name="aggregateTransformer">Generic transformer function that will be called if aggregate exists.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns success if aggregate has been found and transformer executed without errors.</returns>
        Task<Result> Borrow<T>(AggregateId aggregateId, Func<T, Result<T>> aggregateTransformer) where T : AggregateRoot, new();
    }
}