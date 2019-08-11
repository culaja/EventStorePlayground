using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using DomainServices;
using EventStoreAdapter.Writing;
using EventStoreRepository;
using Microsoft.Extensions.Configuration;
using Ports;
using static WebApp.ApplicationWrapping.ConfigurationReader;

namespace WebApp.ApplicationWrapping
{
    public static class WrappedUpCommandExecutors
    {
        public static Func<ICommand, Task<Result>> BookCommandExecutorsWith(IConfiguration configuration) => 
            BookCommandExecutors.BookCommandExecutorsWith(
                BuildRepositoryUsing(
                    AConfigurationReaderWith(configuration)));
        
        public static Func<ICommand, Task<Result>> UserCommandExecutorsWith(IConfiguration configuration) => 
            UserCommandExecutors.UserCommandExecutorsWith(
                BuildRepositoryUsing(
                    AConfigurationReaderWith(configuration)));
        
        private static IRepository BuildRepositoryUsing(ConfigurationReader configurationReader) =>
            new Repository(
                new EventStoreAppender(
                    configurationReader.EventStoreConnectionString,
                    configurationReader.EventStoreName));
    }
}