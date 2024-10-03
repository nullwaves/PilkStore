using PilkUI.Rest.Models;
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

        private static string _server => SettingsService.GetPilkServer();
        private static RestRequest _locationsEndpoint => new("locations/");
        
        public RestService()
        {
            _client = CreateClient();
        }

        private static RestClient CreateClient()
        {
            var options = new RestClientOptions(_server);
            return new RestClient(options);
        }

        public static void Restart()
        {
            Instance._client = CreateClient();
        }

        #region Locations
        public async Task<List<Location>?> GetLocationsAsync(string searchText = "")
        {
            var endpoint = _locationsEndpoint;
            if (searchText.Length > 0)
            {
                endpoint = new RestRequest($"locations/?search={Uri.EscapeDataString(searchText)}");
            }
            try
            {
                return await _client.GetAsync<List<Location>>(endpoint);
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
                var request = new RestRequest($"locations/{pk}/");
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
                request.AddStringBody(location.Serialize(), ContentType.Json);
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
                var request = new RestRequest($"/locations/{location.Pk}/", method: Method.Delete);
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

        public async Task<Location?> UpdateLocationImageAsync(Location location, FileResult image)
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

        #endregion

        #region Pilk

        public async Task<List<Pilk>?> GetPilksAsync(string searchText = "")
        {

            var endpoint = new RestRequest("pilk/");
            if (searchText.Length > 0)
            {
                endpoint = new RestRequest($"pilk/?search={Uri.EscapeDataString(searchText)}");
            }
            try
            {
                return await _client.GetAsync<List<Pilk>>(endpoint);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tREST ERROR: {ex.Message}");
            }
            return [];
        }

        public async Task<Pilk?> CreatePilkAsync(Pilk item)
        {
            try
            {
                var request = new RestRequest("pilk/", method: Method.Post);
                request.AddStringBody(item.Serialize(), ContentType.Json);
                var response = await _client.PostAsync<Pilk>(request);
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tREST ERROR: {ex.Message}");
            }
            return null;
        }

        public async Task<Pilk?> GetPilkFromPkAsync(int pk)
        {
            try
            {
                var request = new RestRequest($"pilk/{pk}/");
                return await _client.GetAsync<Pilk>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tREST ERROR: {ex.Message}");
            }
            return new();
        }

        public async Task<Pilk?> UpdatePilkImageAsync(Pilk pilk, FileResult image)
        {
            try
            {
                var request = new RestRequest($"/pilk/{pilk.Pk}/", method: Method.Patch);
                request.AddStringBody(pilk.Serialize(), ContentType.Json);
                request.AddFile("image", image.FullPath, image.ContentType);
                var response = await _client.PatchAsync<Pilk>(request);
                if (response is not null)
                    return response;
            }
            catch (Exception ex)
            {
                Debug.Write($"\tREST ERROR: {ex.Message}\n{ex.StackTrace}");
            }
            return null;
        }

        public async Task<bool> DeletePilkAsync(Pilk pilk)
        {
            try
            {
                var request = new RestRequest($"/pilk/{pilk.Pk}/", method: Method.Delete);
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

        public async Task<Pilk?> UpdatePilkAsync(Pilk pilk)
        {
            try
            {
                var request = new RestRequest($"pilk/{pilk.Pk}/", method: Method.Patch);
                request.AddStringBody(pilk.Serialize(), ContentType.Json);
                var response = await _client.PatchAsync<Pilk>(request);
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tREST ERROR: {ex.Message}");
            }
            return null;
        }

        #endregion

    }
}
