using System.Text.Json;
using System.Text.Json.Serialization;

namespace ItExpertTestApi
{
    public static class JsonUtils
    {
        public static JsonConverter<T> GetDefaultConverter<T>()
        {
            return (JsonConverter<T>)JsonSerializerOptions.Default
                .GetConverter(typeof(T));
        }
    }
}
