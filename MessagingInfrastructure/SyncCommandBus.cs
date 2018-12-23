using System;
using System.Collections.Concurrent;
using Common;
using Common.Commanding;

namespace MessagingInfrastructure
{
    public sealed class SyncCommandBus<T> : ICommandBus<T> where T : AggregateRoot
    {
        private readonly IRepository<T> _repository;
        private readonly object _syncObject = new object();

        public SyncCommandBus(IRepository<T> repository)
        {
            _repository = repository;
        }
        
        public void Enqueue(INewAggregateRootCommand<T> newAggregateRootCommand)
        {
            lock (_syncObject)
            {
                _repository.AddNew(newAggregateRootCommand.CreateNew());
            }
        }

        public void Enqueue(IAggregateRootCommand<T> aggregateRootCommand)
        {
            lock (_syncObject)
            {
                _repository.BorrowEachFor(
                    aggregateRootCommand.Specification,
                    a => a.Execute(aggregateRootCommand));
            }
        }
    }
}