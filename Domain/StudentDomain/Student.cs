using System;
using Common;
using Common.Commanding;
using Domain.StudentDomain.Commands;
using Domain.StudentDomain.Events;

namespace Domain.StudentDomain
{
    public class Student : AggregateRoot
    {
        public EmailAddress EmailAddress { get; }
        public Maybe<City> MaybeCity { get; private set; } = Maybe<City>.None;
        public bool IsHired { get; private set; } = false;
        
        public Student(Guid id, EmailAddress emailAddress) 
            : base(id)
        {
            EmailAddress = emailAddress;
        }

        public Student MoveTo(City city)
        {
            MaybeCity = city;
            Add(new StudentMoved(Id, city));
            return this;
        }

        public Student Apply(StudentMoved studentMoved)
        {
            MaybeCity = studentMoved.City;
            return this;
        }

        public Student GetAJob()
        {
            IsHired = true;
            Add(new StudentHired(Id));
            return this;
        }

        public Student Apply(StudentHired studentHired)
        {
            IsHired = true;
            return this;
        }
    }
}