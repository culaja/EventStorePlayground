using System;
using Common.Messaging;
using Domain.StudentDomain.Commands;
using Microsoft.AspNetCore.Mvc;
using static Domain.EmailAddress;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMessageBus _messageBus;

        public StudentController(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        
        [HttpPost]
        [Route(nameof(AddNew))]
        public void AddNew([FromBody] NewStudentDto newStudentDto) => _messageBus
            .Dispatch(new AddNewStudent(
                newStudentDto.EmailAddress.ToEmailAddress(),
                newStudentDto.Name.ToName(),
                newStudentDto.City.ToMaybeCity(),
                newStudentDto.IsEmployed));

        [HttpPost]
        [Route(nameof(MoveTo))]
        public void MoveTo([FromBody] MoveToDto moveToDto) => _messageBus
            .Dispatch(new MoveStudent(
                moveToDto.EmailAddress.ToEmailAddress(),
                moveToDto.City.ToCity()));

        public void Hire(string emailAddress) => _messageBus
            .Dispatch(new HireStudent(EmailAddressFrom(emailAddress)));
    }
}