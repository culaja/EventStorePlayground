namespace Aggregate.Student.Shared
{
    public interface IStudentCreatedDto : IDomainEventDto
    {
        string Name { get; set; }
        string EmailAddress { get; set; }
        string City { get; set; }
        bool IsHired { get; set; }
    }
}