using System;
using Domain;
using Domain.StudentDomain;
using Domain.StudentDomain.Events;
using static System.Guid;
using static Common.AggregateRoot;
using static Domain.City;
using static Domain.EmailAddress;

namespace Tests
{
    public static class StudentsValues
    {
        public static Guid StankoId = NewGuid();
        
        public static readonly StudentMoved StankoMoved = new StudentMoved(StankoId, NoviSad); 
        public static readonly StudentHired StankoHired = new StudentHired(StankoId);
        
        public static readonly EmailAddress StankoEmailAddress = EmailAddressFrom("culaja@gmail.com");

        public static Student StankoStudent = CreateNewFrom<Student>(StankoId, StankoEmailAddress);
    }
}