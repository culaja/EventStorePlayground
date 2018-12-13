using System;
using Common;

namespace Domain
{
    public class Student : AggregateRoot
    {
        public Maybe<City> MaybeCity { get; private set; } = Maybe<City>.None;
        public bool IsHired { get; private set; } = false;
        
        public Student(Guid id) : base(id)
        {
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