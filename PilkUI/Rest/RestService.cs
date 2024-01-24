
using System.Diagnostics;
using System.Text.Json;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.Rest
{
    public class RestService
    {
        public static readonly RestService Instance = new();

        readonly HttpClient _client;
        readonly JsonSerializerOptions _serializerOptions;

        string _server => SettingsService.GetPilkServer();
        string _locationsEndpoint => $"{_server}/locations/";
        
        public RestService()
        {
            _client = CreateClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }

        private HttpClient CreateClient()
        {
#if __ANDROID__
            return new HttpClient(new Xamarin.Android.Net.AndroidMessageHandler());
#else
            return new HttpClient();
#endif
        }

        public async Task<List<Location>?> GetLocationsAsync()
        {
            var locations = new List<Location>();

            Uri uri = new(_locationsEndpoint);
            Debug.WriteLine($"\tEndpoint: {_locationsEndpoint}");
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

        public async Task<Location?> GetLocationFromUriAsync(Uri uri)
        {
            var location = new Location();
            try
            {
                var response = await _client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    location = JsonSerializer.Deserialize<Location>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tREST ERROR: {ex.Message}");
            }
            return location;
        }
    }
}
