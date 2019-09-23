using System;
using EventStore.ClientAPI;

namespace EventStoreAdapter.Reading
{
    internal class NoLogger : ILogger
    {
        public void Error(string format, params object[] args)
        {
        }

        public void Error(Exception ex, string format, params object[] args)
        {
        }

        public void Info(string format, params object[] args)
        {
        }

        public void Info(Exception ex, string format, params object[] args)
        {
        }

        public void Debug(string format, params object[] args)
        {
        }

        public void Debug(Exception ex, string format, params object[] args)
        {
        }

        public static ILogger NewNoLogger => new NoLogger();
    }
}