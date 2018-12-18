using Domain.StudentDomain.Commands;
using Microsoft.AspNetCore.Mvc;
using static DomainServicesWrapup.ServicesProxy;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpPost]
        [Route(nameof(AddNew))]
        public void AddNew([FromBody] NewStudentDto newStudentDto) => StudentCommandBus
            .Enqueue(new AddNewStudent(
                newStudentDto.EmailAddress.ToEmailAddress(),
                newStudentDto.Name.ToName(),
                newStudentDto.City.ToMaybeCity(),
                newStudentDto.IsEmployed));

        [HttpPost]
        [Route(nameof(MoveTo))]
        public void MoveTo([FromBody] MoveToDto moveToDto) => StudentCommandBus
            .Enqueue(new MoveStudent(
                moveToDto.EmailAddress.ToEmailAddress(),
                moveToDto.City.ToCity()));
    }
}