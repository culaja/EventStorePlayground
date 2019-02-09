using System;
using Common;
using Common.Messaging;
using Domain.StudentDomain;
using Domain.StudentDomain.Commands;
using Ports.Repositories;

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
            .AddNew(AggregateRoot.CreateNewFrom<Student>(
                Guid.NewGuid(),
                c.Name,
                c.EmailAddress,
                c.MaybeCity,
                c.IsEmployed));
    }
}