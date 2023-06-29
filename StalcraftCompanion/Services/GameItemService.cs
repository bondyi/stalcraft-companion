using RestSharp;
using StalcraftCompanion.Model;
using System.Text.Json;

namespace StalcraftCompanion.Services
{
    public class GameItemService : BaseService
    {
        private const string GITHUB_URL = "https://raw.githubusercontent.com/EXBO-Studio/stalcraft-database/main/ru";

        public GameItemService() : base() { }

        public async Task<List<GameItem>> GetGameItems()
        {
            var request = new RestRequest("https://raw.githubusercontent.com/EXBO-Studio/stalcraft-database/main/ru/listing.json");
            var response = await Client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode) return null;

            var gameItems = JsonSerializer.Deserialize<List<GameItem>>(response.Content);

            foreach (var gameItem in gameItems)
            {
                gameItem.Data = GITHUB_URL + gameItem.Data;
                gameItem.Icon = GITHUB_URL + gameItem.Icon;
            }

            return gameItems;
        }

        public async Task<GameItemInfo> GetGameItem(string url)
        {
            var request = new RestRequest(url);
            var response = await Client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode) return null;

            return JsonSerializer.Deserialize<GameItemInfo>(response.Content);
        }
    }
}
