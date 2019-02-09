using System;
using Common;
using Domain;
using Domain.StudentDomain;
using Ports.Repositories;

namespace InMemory
{
    public sealed class StudentInMemoryRepository : InMemoryRepository<Student>, IStudentRepository
    {
        protected override void AddedNew(Student aggregateRoot) => AddNewIndex(aggregateRoot.EmailAddress, aggregateRoot);

        public Student BorrowBy(EmailAddress emailAddress, Func<Student, Student> transformer) => MaybeReadIndex(emailAddress)
            .Ensure(m => m.HasValue, () => throw new AggregateRootDoesntExistForKeyInRepositoryException<Student>(nameof(EmailAddress), emailAddress))
            .Map(transformer)
            .Value;
    }
}