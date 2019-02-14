using System;
using Aggregate.Student.Shared;
using Common;
using StudentViews;

namespace Services.EventHandlers
{
    public sealed class ViewRefreshFromStudentEventHandler : Common.Messaging.EventHandler<StudentEvent>
    {
        private readonly StudentsPerCityView _studentsPerCityView;

        public ViewRefreshFromStudentEventHandler(StudentsPerCityView studentsPerCityView)
        {
            _studentsPerCityView = studentsPerCityView;
        }
        
        public override Result Handle(StudentEvent e)
        {
            _studentsPerCityView.Apply(e);
            Console.WriteLine(e);
            return Result.Ok();
        }
    }
}