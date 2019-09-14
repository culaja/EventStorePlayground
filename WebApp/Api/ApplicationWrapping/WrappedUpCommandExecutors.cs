using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using DomainServices;
using EventStoreAdapter.Writing;
using EventStoreRepository;
using Microsoft.Extensions.Configuration;
using Ports;
using static WebApp.Api.ApplicationWrapping.ConfigurationReader;

namespace WebApp.Api.ApplicationWrapping
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

        public static Func<ICommand, Task<Result>> SagaCommandExecutorsWith(IConfiguration configuration) =>
            SagaCommandExecutors.SagaCommandExecutorsWith(
                BuildRepositoryUsing(
                    AConfigurationReaderWith(configuration)));
        
        private static IRepository BuildRepositoryUsing(ConfigurationReader configurationReader) =>
            new Repository(
                new EventStoreAppender(
                    configurationReader.EventStoreConnectionString,
                    configurationReader.EventStoreName));
    }
}