using Common;
using Domain.StudentDomain;
using FluentAssertions;
using InMemory;
using Ports.Repositories;
using Xunit;
using static Tests.StudentsValues;

namespace Tests.IntegrationTests.InMemoryRepositoryTests
{
    public sealed class StudentInMemoryRepositoryTests
    {
        private readonly IStudentRepository _studentRepository = new StudentInMemoryRepository();
        
        [Fact]
        public void borrow_can_find_added_student()
        {
            _studentRepository.AddNew(StankoStudent);
            _studentRepository.AddNew(MilenkoStudent);
            
            _studentRepository.BorrowBy(StankoEmailAddress, s => s).Id.Should().Be(StankoId);
            _studentRepository.BorrowBy(MilenkoEmailAddress, s => s).Id.Should().Be(MilenkoId);
        }

        [Fact]
        public void borrow_throw_exception_when_student_doesnt_exist()
        {
            _studentRepository.AddNew(MilenkoStudent);

            Assert.Throws<AggregateRootDoesntExistForKeyInRepositoryException<Student>>(() =>
                _studentRepository.BorrowBy(StankoEmailAddress, s => s));
        }
    }
}