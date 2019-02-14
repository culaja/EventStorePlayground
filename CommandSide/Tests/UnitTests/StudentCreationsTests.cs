using System;
using Aggregate.Student.Shared;
using Common;
using Domain;
using Domain.StudentDomain;
using FluentAssertions;
using Xunit;
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
            "Stanko Culaja",
            "culaja@gmail.com",
            NoviSad.ToMaybe().Map(c => c.ToString()),
            false));

        [Fact]
        public void _2() => _newStudent
            .MoveTo(NoviSad)
            .DomainEvents.Should().Contain(new StudentMoved(_newStudent.Id, NoviSad).SetVersion(_newStudent.Version));

        [Fact]
        public void _3() => _newStudent
            .ApplyFrom(new StudentMoved(_newStudent.Id, NoviSad).SetVersion(2)).To<Student>()
            .MaybeCity.Should().Be(NoviSad);

        [Fact]
        public void _4() => _newStudent
            .GetAJob()
            .DomainEvents.Should().Contain(new StudentHired(_newStudent.Id).SetVersion(_newStudent.Version));

        [Fact]
        public void _5() => _newStudent
            .ApplyFrom(new StudentHired(_newStudent.Id).SetVersion(2)).To<Student>()
            .IsHired.Should().BeTrue();
    }
}