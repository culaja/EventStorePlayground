namespace Aggregate.Student.Shared
{
    public interface IStudentMovedDto : IDomainEventDto
    {
        string City { get; set; }
    }
}