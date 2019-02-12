using System;
using Domain;
using Newtonsoft.Json;
using static Domain.EmailAddress;

namespace EventStore.CustomSerializers
{
    public sealed class EmailAddressSerializer : JsonConverter<EmailAddress>
    {
        public override void WriteJson(JsonWriter writer, EmailAddress value, JsonSerializer serializer) =>
            writer.WriteValue(value.ToString());

        public override EmailAddress ReadJson(JsonReader reader, Type objectType, EmailAddress existingValue, bool hasExistingValue, JsonSerializer serializer) =>
            EmailAddressFrom((string)reader.Value);
    }
}