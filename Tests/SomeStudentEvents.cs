using System;
using Domain;
using Domain.StudentDomain;
using Domain.StudentDomain.Events;
using static System.Guid;
using static Domain.City;

namespace Tests
{
    public static class SomeStudentEvents
    {
        public static Guid StankoId = NewGuid();
        
        public static readonly StudentMoved StankoMoved = new StudentMoved(StankoId, NoviSad); 
        public static readonly StudentHired StankoHired = new StudentHired(StankoId);
    }
}