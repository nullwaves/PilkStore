
using PilkUI.Rest.Serializers;
using RestSharp;
using System.Diagnostics;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.Rest
{
    public class RestService
    {
        public static readonly RestService Instance = new();

        private RestClient _client;

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
                var request = new RestRequest($"locations/{pk}");
                return await _client.GetAsync<Location>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tREST ERROR: {ex.Message}");
            }
            return new();
        }

        public async Task<Location?> CreateLocationAsync(Location location)
        {
            try
            {
                var request = new RestRequest("locations/", method:Method.Post);
                // request.AddJsonBody(new Dictionary<string, string>() { { "Name", location.Name }, { "Description", location.Description } });
                var body = location.Serialize();
                Debug.WriteLine(body);
                request.AddStringBody(body, ContentType.Json);
                var response = await _client.PostAsync<Location>(request);
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tREST ERROR: {ex.Message}");
            }
            return null;
        }

        public async Task<Location?> UpdateLocationAsync(Location location)
        {
            try
            {
                var request = new RestRequest($"locations/{location.Pk}/", method: Method.Patch);
                request.AddStringBody(location.Serialize(), ContentType.Json);
                var response = await _client.PatchAsync<Location>(request);
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tREST ERROR: {ex.Message}");
            }
            return null;
        }

        public async Task<bool> DeleteLocationAsync(Location location)
        {
            try
            {
                var request = new RestRequest($"/locations/{location.Pk}", method: Method.Delete);
                var response = await _client.DeleteAsync(request);
                if (response is not null)
                    return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tREST ERROR: {ex.Message}");
            }
            return false;
        }

        internal async Task<Location?> UpdateLocationImageAsync(Location location, FileResult image)
        {
            try
            {
                var request = new RestRequest($"/locations/{location.Pk}/", method: Method.Patch);
                request.AddStringBody(location.Serialize(), ContentType.Json);
                request.AddFile("image", image.FullPath, image.ContentType);
                var response = await _client.PatchAsync<Location>(request);
                if (response is not null)
                    return response;
            }
            catch (Exception ex)
            {
                Debug.Write($"\tREST ERROR: {ex.Message}\n{ex.StackTrace}");
            }
            return null;
        }

        public static void Restart()
        {
            Instance._client = Instance.CreateClient();
        }
    }
}
