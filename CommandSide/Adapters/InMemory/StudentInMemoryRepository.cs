using System;
using Aggregate.Student.Shared;
using Common;
using Common.Messaging;
using Domain;
using Domain.StudentDomain;
using Ports.Repositories;

namespace InMemory
{
    public sealed class StudentInMemoryRepository : InMemoryRepository<Student, StudentCreated>, IStudentRepository
    {
        public StudentInMemoryRepository(ILocalMessageBus localMessageBus) : base(localMessageBus)
        {
        }

        protected override Student CreateInternalFrom(StudentCreated studentCreated) =>
            new Student(
                studentCreated.AggregateRootId,
                studentCreated.Version,
                studentCreated.Name.ToName(),
                studentCreated.EmailAddress.ToEmailAddress(),
                studentCreated.MaybeCity.Map(s => s.ToCity()),
                studentCreated.IsHired);

        protected override void AddedNew(Student aggregateRoot) => AddNewIndex(aggregateRoot.EmailAddress, aggregateRoot);

        protected override bool ContainsKey(Student aggregateRoot) => ContainsIndex(aggregateRoot.EmailAddress);

        public Result<Student> BorrowBy(EmailAddress emailAddress, Func<Student, Student> transformer) => MaybeReadIndex(emailAddress)
            .OnSuccess(t=> ExecuteTransformerAndPurgeEvents(t, transformer));
    }
}