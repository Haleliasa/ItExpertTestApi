using System.Text.Json;

namespace ItExpertTestApi.Items
{
    /// <summary>
    /// Supports { "1": "value1" } and { "code": 1, "value": "value1" } formats
    /// </summary>
    public class ItemInJsonConverter : ReadOnlyJsonConverter<ItemIn>
    {
        public override ItemIn? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            ItemIn item = new(default, "");

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return item;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                string? property = reader.GetString();
                reader.Read();

                if (string.IsNullOrEmpty(property))
                {
                    continue;
                }

                if (int.TryParse(property, out int codeProp))
                {
                    string? value = reader.GetString()
                        ?? throw new JsonException();
                    item = item with { Code = codeProp, Value = value };
                    continue;
                }

                if (property.Equals(
                    nameof(ItemIn.Code),
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!reader.TryGetInt32(out int code))
                    {
                        throw new JsonException();
                    }
                    item = item with { Code = code };
                    continue;
                }

                if (property.Equals(
                    nameof(ItemIn.Value),
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    string? value = reader.GetString()
                        ?? throw new JsonException();
                    item = item with { Value = value };
                    continue;
                }
            }

            throw new JsonException();
        }
    }
}
