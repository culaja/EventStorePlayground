using System;
using Common;
using Domain;
using Newtonsoft.Json;
using static Common.Maybe<Domain.City>;

namespace EventStore
{
    public sealed class MaybeCitySerializer : JsonConverter<Maybe<City>>
    {
        public override void WriteJson(JsonWriter writer, Maybe<City> value, JsonSerializer serializer) => 
            writer.WriteValue(value.HasValue ? (object)value.Value : (object)0);

        public override Maybe<City> ReadJson(JsonReader reader, Type objectType, Maybe<City> existingValue, bool hasExistingValue, JsonSerializer serializer) => 
            From((City)reader.Value);
    }
}