using Common;
using Domain;
using Domain.StudentDomain;

namespace InMemory
{
    public sealed class StudentInMemoryRepository : InMemoryRepository<Student>
    {
        protected override void AddedNew(Student aggregateRoot) => AddNewIndex(aggregateRoot.EmailAddress, aggregateRoot);

        public Maybe<Student> GetBy(EmailAddress emailAddress) => ReadIndex(emailAddress);
    }
}