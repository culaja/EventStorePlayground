using Shared.Common;

namespace Aggregate.Student.Shared
{
    public sealed class StudentMovedShared : SharedEvent
    {
        public string City { get; set; }
    }
}