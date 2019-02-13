using Common;
using Common.Messaging;
using Domain.StudentDomain.Commands;
using Ports.Repositories;

namespace DomainServices.StudentHandlers.Commands
{
    public sealed class MoveStudentHandler : CommandHandler<MoveStudent>
    {
        private readonly IStudentRepository _studentRepository;

        public MoveStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public override Result Handle(MoveStudent c) => _studentRepository
            .BorrowBy(c.EmailAddress, s => s.MoveTo(c.CityToMoveTo));
    }
}