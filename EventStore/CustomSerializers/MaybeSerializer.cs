using System;
using Common;
using Domain;
using Newtonsoft.Json;

namespace EventStore.CustomSerializers
{
    public sealed class MaybeCitySerializer : JsonConverter<Maybe<City>>
    {
        public override void WriteJson(JsonWriter writer, Maybe<City> value, JsonSerializer serializer) => 
            writer.WriteValue(value.HasValue ? value.Value.ToString() : null);

        public override Maybe<City> ReadJson(JsonReader reader, Type objectType, Maybe<City> existingValue, bool hasExistingValue, JsonSerializer serializer) =>
            reader.Value == null ? Maybe<City>.None : Maybe<City>.From(City.CityFrom((string)reader.Value));
    }
}