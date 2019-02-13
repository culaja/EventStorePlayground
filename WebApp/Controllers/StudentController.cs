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
        private readonly ILocalMessageBus _localMessageBus;

        public StudentController(ILocalMessageBus localMessageBus)
        {
            _localMessageBus = localMessageBus;
        }
        
        [HttpPost]
        [Route(nameof(AddNew))]
        public void AddNew([FromBody] NewStudentDto newStudentDto) => _localMessageBus
            .Dispatch(new AddNewStudent(
                newStudentDto.EmailAddress.ToEmailAddress(),
                newStudentDto.Name.ToName(),
                newStudentDto.City.ToMaybeCity(),
                newStudentDto.IsEmployed));

        [HttpPost]
        [Route(nameof(MoveTo))]
        public void MoveTo([FromBody] MoveToDto moveToDto) => _localMessageBus
            .Dispatch(new MoveStudent(
                moveToDto.EmailAddress.ToEmailAddress(),
                moveToDto.City.ToCity()));

        [HttpPost]
        [Route(nameof(Hire))]
        public void Hire(string emailAddress) => _localMessageBus
            .Dispatch(new HireStudent(EmailAddressFrom(emailAddress)));
    }
}