using System;
using System.Collections.Generic;
using System.Text;
using Aggregate.Student.Shared;
using Common;
using QueryCommon;

namespace StudentViews
{
    public sealed class StudentsPerCityView : View,
        IHandle<StudentCreated>,
        IHandle<StudentMoved>
    {
        private readonly Dictionary<Guid, string> _emailAddressPerStudent = new Dictionary<Guid, string>();
        private readonly Dictionary<string, HashSet<string>> _studentsPerCityDict = new Dictionary<string, HashSet<string>>();

        public void Handle(StudentCreated e)
        {
            _emailAddressPerStudent[e.AggregateRootId] = e.EmailAddress;
            if (e.MaybeCity.HasValue)
            {
                GetStudentsFor(e.MaybeCity.Value).Add(e.EmailAddress);
            }
        }

        public void Handle(StudentMoved e)
        {
            var emailAddress = _emailAddressPerStudent[e.AggregateRootId];
            
            if (e.MaybeMovedFromCity.HasValue)
            {
                GetStudentsFor(e.MaybeMovedFromCity.Value).Remove(emailAddress);
            }

            GetStudentsFor(e.MovedToCity).Add(emailAddress);
        }

        private HashSet<string> GetStudentsFor(string city)
        {
            if (!_studentsPerCityDict.TryGetValue(city, out var students))
            {
                students = new HashSet<string>();
                _studentsPerCityDict[city] = students;
            }

            return students;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            
            builder.AppendLine($"We have total of {_studentsPerCityDict.Count} cities mentioned and statistics are:");
            foreach (var keyValue in _studentsPerCityDict)
            {
                builder.Append($"{keyValue.Key}: ");
                foreach (var emailAddress in keyValue.Value)
                {
                    builder.Append($"{emailAddress}, ");
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}