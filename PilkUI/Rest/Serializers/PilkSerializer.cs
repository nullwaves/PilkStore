using PilkUI.Rest.Models;
using System.Text.Json;

namespace PilkUI.Rest.Serializers
{
    public static class PilkSerializer
    {
        private static JsonSerializerOptions _serializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        public static string Serialize(this Pilk item)
        {
            Dictionary<string, object> dict = new()
            {
                { "pk", item.Pk },
                { "location", item.Location },
                { "name", item.Name },
                { "description", item.Description },
            };
            return JsonSerializer.Serialize(dict, _serializerOptions);
        }
    }
}
