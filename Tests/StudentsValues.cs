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
        
        public static readonly StudentMoved StankoMovedToNoviSad = new StudentMoved(StankoId, NoviSad); 
        public static readonly StudentHired StankoHired = new StudentHired(StankoId);
        
        public static readonly EmailAddress StankoEmailAddress = EmailAddressFrom("culaja@gmail.com");

        public static readonly Student StankoStudent = CreateNewFrom<Student>(StankoId, StankoEmailAddress);

        public static Guid MilenkoId = NewGuid();
        
        public static readonly StudentMoved MilenkoMovedToBelgrade = new StudentMoved(MilenkoId, Belgrade);
        public static readonly StudentHired MilenkoHired = new StudentHired(MilenkoId);
        
        public static readonly EmailAddress MilenkoEmailAddress = EmailAddressFrom("j.milenko@gmail.com");

        public static readonly Student MilenkoStudent = CreateNewFrom<Student>(MilenkoId, MilenkoEmailAddress);
    }
}