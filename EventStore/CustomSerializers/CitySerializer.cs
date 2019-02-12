using System;
using Domain;
using Newtonsoft.Json;
using static Domain.City;

namespace EventStore.CustomSerializers
{
    public sealed class CitySerializer : JsonConverter<City>
    {
        public override void WriteJson(JsonWriter writer, City value, JsonSerializer serializer) =>
            writer.WriteValue(value.ToString());

        public override City ReadJson(JsonReader reader, Type objectType, City existingValue, bool hasExistingValue, JsonSerializer serializer) =>
            CityFrom((string)reader.Value);
    }
}