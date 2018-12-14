using Common;
using Domain;
using FluentAssertions;
using Xunit;

namespace Tests.UnitTests
{
    public sealed class StudentCreationsTests
    {
        private readonly Student _newStudent = AggregateRoot.CreateNew<Student>();
        
        [Fact]
        public void _1() => _newStudent.DomainEvents.Should().Contain(new AggregateRootCreated(
            _newStudent.Id,
            typeof(Student)));

        [Fact]
        public void _2() => _newStudent
            .MoveTo(City.NoviSad)
            .DomainEvents.Should().Contain(new StudentMoved(_newStudent.Id, City.NoviSad));

        [Fact]
        public void _3() => new StudentMoved(_newStudent.Id, City.NoviSad)
            .ApplyTo(_newStudent)
            .MaybeCity.Should().Be(City.NoviSad);

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