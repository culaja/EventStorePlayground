using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using EventStoreAdapter;
using EventStoreAdapter.Writing;
using EventStoreRepository;
using Microsoft.Extensions.Configuration;
using Ports;
using static DomainServices.DomainCommandExecutors;
using static WebApp.ApplicationWrapping.ConfigurationReader;

namespace WebApp.ApplicationWrapping
{
    public static class WrappedUpCommandExecutors
    {
        public static Func<ICommand, Task<Result>> DomainCommandExecutorsWith(IConfiguration configuration) => 
            CommandExecutorsWith(
                BuildRepositoryUsing(
                    AConfigurationReaderWith(configuration)));
        
        private static IRepository BuildRepositoryUsing(ConfigurationReader configurationReader) =>
            new Repository(
                new EventStoreAppender(
                    configurationReader.EventStoreConnectionString,
                    configurationReader.EventStoreName));
    }
}