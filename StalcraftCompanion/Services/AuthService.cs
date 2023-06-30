using RestSharp;
using System.Text;
using System.Text.Json;

namespace StalcraftCompanion.Services
{
    public class AuthService : BaseService
    {
        private const int CLIENT_ID = 209;
        private const string CLIENT_SECRET = "tMMhRiygrVASScbRchgHbSvWIYqbbcKZyBjwFjIh";

        public AuthService() : base() { }

        public async void GetAppAccessToken()
        {
            var request = new RestRequest("https://exbo.net/oauth/token", Method.Post);

            request.AddParameter("client_id", CLIENT_ID);
            request.AddParameter("client_secret", CLIENT_SECRET);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("scope", "");

            var response = await Client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode) return;

            var accessToken = JsonSerializer.Deserialize<Dictionary<string, object>>(response.Content);

            await SecureStorage.Default.SetAsync("app_access_token", accessToken.GetValueOrDefault("access_token").ToString());
        }

        public async void GetUserTokens()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("https://exbo.net/oauth/authorize");
            stringBuilder.AppendLine($"?client_id={CLIENT_ID}");
            stringBuilder.AppendLine("&redirect_uri=http://localhost");
            stringBuilder.AppendLine("&scope=");
            stringBuilder.AppendLine("&response_type=code");

            try
            {
                WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri(stringBuilder.ToString()),
                    new Uri("stalcraftcompanion://"));

                var code = authResult?.Get("code");

                var request = new RestRequest("https://exbo.net/oauth/token", Method.Post);

                request.AddQueryParameter("client_id", CLIENT_ID);
                request.AddQueryParameter("client_secret", CLIENT_SECRET);
                request.AddQueryParameter("client_secret", code);
                request.AddQueryParameter("grant_type", "authorization_code");
                request.AddQueryParameter("redirect_uri", "http://localhost");

                var response = await Client.ExecuteAsync(request);

                if (!response.IsSuccessStatusCode) return;

                var tokens = JsonSerializer.Deserialize<Dictionary<string, string>>(response.Content);

                await SecureStorage.Default.SetAsync("user_access_token", tokens.GetValueOrDefault("access_token"));
                await SecureStorage.Default.SetAsync("user_refresh_token", tokens.GetValueOrDefault("refresh_token"));
            }
            catch (TaskCanceledException)
            {
                
            }
        }

        public async void RefreshUserTokens()
        {
            var request = new RestRequest("https://exbo.net/oauth/token", Method.Post);

            request.AddQueryParameter("client_id", CLIENT_ID);
            request.AddQueryParameter("client_secret", CLIENT_SECRET);
            request.AddQueryParameter("grant_type", "refresh_token");
            request.AddQueryParameter("refresh_token", await SecureStorage.Default.GetAsync("user_refresh_token"));
            request.AddQueryParameter("scope", "");

            var response = await Client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode) return;

            var tokens = JsonSerializer.Deserialize<Dictionary<string, string>>(response.Content);

            await SecureStorage.Default.SetAsync("user_access_token", tokens.GetValueOrDefault("access_token"));
            await SecureStorage.Default.SetAsync("user_refresh_token", tokens.GetValueOrDefault("refresh_token"));
        }
    }
}
