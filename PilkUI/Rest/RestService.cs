
using System.Diagnostics;
using System.Text.Json;

namespace PilkUI.Rest
{


    public class RestService
    {
        public static RestService Instance = new();

        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        
        public RestService()
        {
            _client = new();
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }

        public async Task<List<Location>> GetLocationsAsync()
        {
            var locations = new List<Location>();

            Uri uri = new Uri("http://localhost:8000/locations");
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    locations = JsonSerializer.Deserialize<List<Location>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tREST ERROR: {ex.Message}");
            }
            return locations;
        }
    }
}
