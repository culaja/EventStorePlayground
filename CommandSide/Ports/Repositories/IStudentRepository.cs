using System;
using Aggregate.Student.Shared;
using Common;
using Domain;
using Domain.StudentDomain;

namespace Ports.Repositories
{
    public interface IStudentRepository : IRepository<Student, StudentCreated>
    {
        Result<Student> BorrowBy(EmailAddress emailAddress, Func<Student, Student> transformer);
    }
}