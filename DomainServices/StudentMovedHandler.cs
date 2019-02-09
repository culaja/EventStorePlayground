using Common;
using Common.Messaging;
using Domain.StudentDomain.Events;

namespace DomainServices
{
    public sealed class StudentMovedHandler : EventHandler<StudentMoved>
    {
        public override Result Handle(StudentMoved message)
        {
            throw new System.NotImplementedException();
        }
    }
}