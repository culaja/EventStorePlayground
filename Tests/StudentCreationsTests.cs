using Common;
using Domain;
using FluentAssertions;
using Xunit;
using static Common.AggregateRoot;
using static Domain.City;

namespace Tests
{
    public sealed class StudentCreationsTests
    {
        private readonly Student _newStudent = CreateNew<Student>();
        
        [Fact]
        public void _1() => _newStudent.DomainEvents.Should().Contain(new AggregateRootCreated(
            _newStudent.Id,
            typeof(Student)));

        [Fact]
        public void _2() => _newStudent
            .MoveTo(NoviSad)
            .DomainEvents.Should().Contain(new StudentMoved(_newStudent.Id, NoviSad));

        [Fact]
        public void _3() => new StudentMoved(_newStudent.Id, NoviSad)
            .ApplyTo(_newStudent)
            .MaybeCity.Should().Be(NoviSad);

        [Fact]
        public void _4() => _newStudent
            .GetAJob()
            .DomainEvents.Should().Contain(new StudentHired(_newStudent.Id));

        [Fact]
        public void _5() => new StudentHired(_newStudent.Id)
            .ApplyTo(_newStudent)
            .IsHired.Should().BeTrue();
    }
}