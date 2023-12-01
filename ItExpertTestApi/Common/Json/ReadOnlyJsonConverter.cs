using System.Text.Json;
using System.Text.Json.Serialization;

namespace ItExpertTestApi
{
    public abstract class ReadOnlyJsonConverter<T> : JsonConverter<T>
    {
        public sealed override void Write(
            Utf8JsonWriter writer,
            T value,
            JsonSerializerOptions options)
        {
            JsonConverter<T> converter = JsonUtils.GetDefaultConverter<T>();
            converter.Write(writer, value, options);
        }
    }
}
