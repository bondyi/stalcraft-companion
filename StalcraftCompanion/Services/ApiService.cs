using RestSharp;
using StalcraftCompanion.Model;
using System.Text.Json;

namespace StalcraftCompanion.Services
{
    public class ApiService : BaseService
    {
        private const string BASE_URL = "https://eapi.stalcraft.net/";

        private readonly AuthService authService;

        public ApiService(AuthService service) : base()
        {
            authService = service;
        }

        public async Task<Emission> GetEmissionStatus()
        {
            if (await SecureStorage.Default.GetAsync("app_access_token") == null)
            {
                authService.GetAppAccessToken();
            }

            var request = new RestRequest(BASE_URL + "eu/emission");
            request.AddHeader("Authorization", $"Bearer {await SecureStorage.Default.GetAsync("app_access_token")}");

            var response = await Client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode) return null;

            var emission = JsonSerializer.Deserialize<Emission>(response.Content);

            return emission;
        }
    }
}
