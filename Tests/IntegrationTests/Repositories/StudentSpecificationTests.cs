using Common;
using Domain.StudentDomain;
using Domain.StudentDomain.Specifications;
using FluentAssertions;
using InMemory;
using Xunit;
using static Tests.StudentsValues;

namespace Tests.IntegrationTests.Repositories
{
    public sealed class StudentSpecificationTests
    {
        private readonly IRepository<Student> _studentRepository = new StudentInMemoryRepository();

        [Fact]
        public void _1()
        {
            _studentRepository.AddNew(StankoStudent);

            _studentRepository.BorrowEachFor(new StudentByEmailAddressSpecification(StankoEmailAddress), s => s)
                .Id.Should().Be(StankoId);
        }
    }
}