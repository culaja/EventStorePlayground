using System;
using Common;
using Domain;
using Domain.StudentDomain;

namespace Ports.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student BorrowBy(EmailAddress emailAddress,Func<Student, Student> transformer);
    }
}