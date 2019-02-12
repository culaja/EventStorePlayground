using System;
using Common;
using Domain;
using Domain.StudentDomain;
using Domain.StudentDomain.Events;
using FluentAssertions;
using Xunit;
using static Common.AggregateRoot;
using static Domain.City;
using static Domain.EmailAddress;
using static Domain.Name;
using static Domain.StudentDomain.Student;

namespace Tests.UnitTests
{
    public sealed class StudentCreationsTests
    {
        private readonly Student _newStudent = NewStudentFrom(
            Guid.NewGuid(),
            NameFrom("Stanko Culaja"),
            EmailAddressFrom("culaja@gmail.com"),
            Maybe<City>.From(NoviSad),
            false);
        
        [Fact]
        public void _1() => _newStudent.DomainEvents.Should().Contain(new StudentCreated(
            _newStudent.Id,
            typeof(Student),
            NameFrom("Stanko Culaja"),
            EmailAddressFrom("culaja@gmail.com"),
            Maybe<City>.From(NoviSad),
            false));

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