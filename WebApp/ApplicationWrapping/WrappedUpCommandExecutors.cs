using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using EventStoreAdapter;
using EventStoreRepository;
using LocalMessageBusAdapter;
using Microsoft.Extensions.Configuration;
using Ports;
using static DomainServices.DomainCommandExecutors;
using static DomainServices.DomainEventHandlers;
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
                new MyEventStore(
                    configurationReader.EventStoreConnectionString,
                    configurationReader.EventStoreName,
                    LocalMessageBusWith()));
        
        private static ILocalMessageBus LocalMessageBusWith() =>
            new LocalMessageBus(
                m => DomainEventHandlersWith()(m as IDomainEvent));
    }
}