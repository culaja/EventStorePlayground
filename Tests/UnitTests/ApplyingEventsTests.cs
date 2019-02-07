using System;
using Common;
using Domain;
using Domain.StudentDomain;
using Domain.StudentDomain.Events;
using FluentAssertions;
using InMemory;
using Ports.EventStore;
using Xunit;
using static Domain.EmailAddress;

namespace Tests.UnitTests
{
    public sealed class ApplyingEventsTests
    {
        private readonly IEventStore _eventStore = new InMemoryEventStore();
        private readonly IRepository<Student> _studentRepository = new StudentInMemoryRepository();

        [Fact]
        public void _1()
        {
            var studentId = Guid.NewGuid();
            _eventStore.Append(new AggregateRootCreated(typeof(Student), studentId, EmailAddressFrom("culaja@gmail.com")));
            _eventStore.Append(new StudentMoved(studentId, City.Belgrade));
            _eventStore.Append(new StudentHired(studentId));

            _eventStore.ApplyAllTo(_studentRepository);

            var student = _studentRepository.Borrow(studentId, s => s);
            
            student.Id.Should().Be(studentId);
            student.MaybeCity.Should().Be(City.Belgrade);
            student.IsHired.Should().BeTrue();
        }
    }
}