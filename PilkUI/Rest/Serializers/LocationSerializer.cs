using System.Text.Json;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.Rest.Serializers
{
    public static class LocationSerializer
    {
        public static JsonSerializerOptions _serializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        public static string Serialize(this Location location)
        {
            var dict = new Dictionary<string, object>()
            {
                {"name", location.Name },
                {"description", location.Description },
                //{"image", "" },
                {"children", Array.Empty<string>() },
                {"items", Array.Empty<string>() },
            };
            return JsonSerializer.Serialize(dict, _serializerOptions);
        }
    }
}
