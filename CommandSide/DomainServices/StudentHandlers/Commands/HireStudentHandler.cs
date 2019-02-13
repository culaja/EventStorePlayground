using Common;
using Common.Messaging;
using Domain.StudentDomain.Commands;
using Ports.Repositories;

namespace DomainServices.StudentHandlers.Commands
{
    public sealed class HireStudentHandler : CommandHandler<HireStudent>
    {
        private readonly IStudentRepository _studentRepository;

        public HireStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public override Result Handle(HireStudent c) => _studentRepository
            .BorrowBy(c.EmailAddress, s => s.GetAJob());
    }
}