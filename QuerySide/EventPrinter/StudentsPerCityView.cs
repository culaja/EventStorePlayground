using System;
using System.Collections.Generic;
using System.Text;
using Aggregate.Student.Shared;
using Common;
using QueryCommon;

namespace EventPrinter
{
    public sealed class StudentsPerCityView : View,
        IHandle<StudentCreated>,
        IHandle<StudentMoved>
    {
        private readonly Dictionary<string, int> _studentPerCityDict = new Dictionary<string, int>();

        public void Handle(StudentCreated e)
        {
            e.MaybeCity.Map(IncrementStudentIn);
        }

        public void Handle(StudentMoved e)
        {
            e.MaybeMovedFromCity.Map(DecrementStudentIn);
            IncrementStudentIn(e.MovedToCity);
        }

        private string IncrementStudentIn(string city)
        {
            if (_studentPerCityDict.TryGetValue(city, out var count))
            {
                _studentPerCityDict[city] = count + 1;
            }
            else
            {
                _studentPerCityDict[city] = 1;
            }

            return city;
        }

        private string DecrementStudentIn(string city)
        {
            if (!_studentPerCityDict.TryGetValue(city, out var count))
            {
                throw new InvalidOperationException($"Trying to decrement student from city '{city}' even if city was not mentioned before!");
            }

            _studentPerCityDict[city] = count - 1;
            return city;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            
            builder.AppendLine($"We have total of {_studentPerCityDict.Count} cities mentioned and statistics are:");
            foreach (var keyValue in _studentPerCityDict)
            {
                builder.AppendLine($"{keyValue.Key}: {keyValue.Value}");
            }

            return builder.ToString();
        }
    }
}