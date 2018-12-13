using Common;
using Domain;
using FluentAssertions;
using Xunit;
using static Common.AggregateRoot;

namespace Tests
{
    public sealed class StudentCreationsTests
    {
        private readonly Student _newStudent = CreateNew<Student>();
        
        [Fact]
        public void _1() => _newStudent.DomainEvents.Should().Contain(new AggregateRootCreated(
            _newStudent.Id,
            typeof(Student)));
    }
}