using System;
using Common;

namespace Domain
{
    public class Student : AggregateRoot
    {
        public Student(Guid id) : base(id)
        {
        }
    }
}