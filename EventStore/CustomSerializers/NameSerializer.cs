using System;
using Domain;
using Newtonsoft.Json;

namespace EventStore.CustomSerializers
{
    public sealed class NameSerializer : JsonConverter<Name>
    {
        public override void WriteJson(JsonWriter writer, Name value, JsonSerializer serializer) =>
            writer.WriteValue(value.ToString());

        public override Name ReadJson(JsonReader reader, Type objectType, Name existingValue, bool hasExistingValue, JsonSerializer serializer) =>
            Name.NameFrom((string)reader.Value);
    }
}