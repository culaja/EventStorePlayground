using Common;
using Common.Messaging;
using Domain.StudentDomain.Commands;
using Ports.Repositories;
using static System.Guid;
using static Domain.StudentDomain.Student;

namespace DomainServices.StudentHandlers.Commands
{
    public class AddNewStudentHandler : CommandHandler<AddNewStudent>
    {
        private readonly IStudentRepository _studentRepository;

        public AddNewStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public override Result Handle(AddNewStudent c) => _studentRepository
            .AddNew(NewStudentFrom(
                NewGuid(),
                c.Name,
                c.EmailAddress,
                c.MaybeCity,
                c.IsEmployed));
    }
}