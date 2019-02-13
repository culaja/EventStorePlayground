using System;
using Common;
using Common.Messaging;
using Domain;
using Domain.StudentDomain;
using Domain.StudentDomain.Events;
using Ports.Repositories;

namespace InMemory
{
    public sealed class StudentInMemoryRepository : InMemoryRepository<Student, StudentCreated>, IStudentRepository
    {
        public StudentInMemoryRepository(IMessageBus messageBus) : base(messageBus)
        {
        }

        protected override Student CreateInternalFrom(StudentCreated studentCreated) =>
            new Student(
                studentCreated.AggregateRootId,
                studentCreated.Version,
                studentCreated.Name,
                studentCreated.EmailAddress,
                studentCreated.MaybeCity,
                studentCreated.IsHired);

        protected override void AddedNew(Student aggregateRoot) => AddNewIndex(aggregateRoot.EmailAddress, aggregateRoot);

        protected override bool ContainsKey(Student aggregateRoot) => ContainsIndex(aggregateRoot.EmailAddress);

        public Result<Student> BorrowBy(EmailAddress emailAddress, Func<Student, Student> transformer) => MaybeReadIndex(emailAddress)
            .OnSuccess(t=> ExecuteTransformerAndPurgeEvents(t, transformer));
    }
}