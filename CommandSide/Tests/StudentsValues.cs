using System;
using Common;
using Domain;
using Domain.StudentDomain;
using Domain.StudentDomain.Events;
using static System.Guid;
using static Domain.City;
using static Domain.EmailAddress;
using static Domain.Name;
using static Domain.StudentDomain.Student;

namespace Tests
{
    public static class StudentsValues
    {
        public static Guid StankoId = NewGuid();
        public static readonly Name StankoName = NameFrom("Stanko Culaja");
        
        public static readonly StudentMoved StankoMovedToNoviSad = new StudentMoved(StankoId, NoviSad); 
        public static readonly StudentHired StankoHired = new StudentHired(StankoId);
        
        public static readonly EmailAddress StankoEmailAddress = EmailAddressFrom("culaja@gmail.com");
        
        public static readonly StudentCreated StankoCreated = new StudentCreated(
            StankoId,
            StankoName,
            StankoEmailAddress,
            Maybe<City>.None, 
            false);

        public static readonly Student StankoStudent = NewStudentFrom(
            StankoId,
            StankoName,
            StankoEmailAddress,
            Maybe<City>.None,
            false);

        public static Guid MilenkoId = NewGuid();

        public static readonly Name MilenkoName = NameFrom("Milenko Jovanovic"); 
        public static readonly StudentMoved MilenkoMovedToBelgrade = new StudentMoved(MilenkoId, Belgrade);
        public static readonly StudentHired MilenkoHired = new StudentHired(MilenkoId);
        
        public static readonly EmailAddress MilenkoEmailAddress = EmailAddressFrom("j.milenko@gmail.com");
        
        public static readonly StudentCreated MilenkoCreated = new StudentCreated(
            StankoId,
            StankoName,
            StankoEmailAddress,
            Belgrade, 
            false);

        public static readonly Student MilenkoStudent = NewStudentFrom(
            MilenkoId,
            MilenkoName,
            MilenkoEmailAddress,
            Belgrade,
            false);
    }
}