using System;
using System.Linq.Expressions;
using Common.Commanding;

namespace Domain.StudentDomain.Specifications
{
    public sealed class StudentByEmailAddressSpecification : ISpecification<Student>
    {
        public EmailAddress EmailAddress { get; }

        public StudentByEmailAddressSpecification(EmailAddress emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public Expression<Func<Student, bool>> IsSatisfied() =>
            student => student.EmailAddress.Equals(EmailAddress);
    }
}