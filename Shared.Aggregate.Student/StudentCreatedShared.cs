using Shared.Common;

namespace Aggregate.Student.Shared
{
    public sealed class StudentCreatedShared : SharedEvent
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string City { get; set; }
        public bool IsHired { get; set; }
    }
}