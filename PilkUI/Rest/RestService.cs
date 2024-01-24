
using RestSharp;
using System.Diagnostics;
using System.Text.Json;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.Rest
{
    public class RestService
    {
        public static readonly RestService Instance = new();

        readonly RestClient _client;

        string _server => SettingsService.GetPilkServer();
        RestRequest _locationsEndpoint => new("locations/");
        
        public RestService()
        {
            _client = CreateClient();
        }

        private RestClient CreateClient()
        {
            var options = new RestClientOptions(_server);
            return new RestClient(options); 
        }

        public async Task<List<Location>?> GetLocationsAsync()
        {
            Debug.WriteLine($"\tEndpoint: {_locationsEndpoint}");
            try
            {
                return await _client.GetAsync<List<Location>>(_locationsEndpoint);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tREST ERROR: {ex.Message}");
            }
            return [];
        }

        public async Task<Location?> GetLocationFromPkAsync(int pk)
        {
            try
            {
                var request = new RestRequest($"/locations/{pk}");
                return await _client.GetAsync<Location>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tREST ERROR: {ex.Message}");
            }
            return new();
        }
    }
}
