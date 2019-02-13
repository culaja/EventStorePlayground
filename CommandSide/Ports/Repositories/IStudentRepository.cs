using System;
using Common;
using Domain;
using Domain.StudentDomain;
using Domain.StudentDomain.Events;

namespace Ports.Repositories
{
    public interface IStudentRepository : IRepository<Student, StudentCreated>
    {
        Result<Student> BorrowBy(EmailAddress emailAddress, Func<Student, Student> transformer);
    }
}