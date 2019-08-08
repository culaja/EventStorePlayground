using Common;
using Microsoft.Extensions.Configuration;

namespace WebApp.ApplicationWrapping
{
    public sealed class ConfigurationReader
    {
        private readonly Maybe<IConfiguration> _maybeConfiguration;

        private ConfigurationReader(Maybe<IConfiguration> maybeConfiguration)
        {
            _maybeConfiguration = maybeConfiguration;
        }

        public static ConfigurationReader AConfigurationReaderWith(IConfiguration maybeConfiguration) =>
            new ConfigurationReader(Maybe<IConfiguration>.From(maybeConfiguration));

        public static ConfigurationReader AConfigurationReaderWithDefaultConfiguration =>
            new ConfigurationReader(Maybe<IConfiguration>.None);

        public string EventStoreConnectionString =>
            GetStringAppConfigItemOrUseDefaultIfDoesntExist(nameof(EventStoreConnectionString), "tcp://localhost:1113");
        
        public string EventStoreName =>
            GetStringAppConfigItemOrUseDefaultIfDoesntExist(nameof(EventStoreName), "Library");
        
        private string GetStringAppConfigItemOrUseDefaultIfDoesntExist(string key, string defaultValue) =>
            _maybeConfiguration
                .Map(configuration => configuration[$"AppSettings:{key}"] ?? defaultValue)
                .Unwrap(
                    value => value,
                    () => defaultValue);
    }
}