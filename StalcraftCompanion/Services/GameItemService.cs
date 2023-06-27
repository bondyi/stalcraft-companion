using StalcraftCompanion.Model;
using System.Net.Http.Json;

namespace StalcraftCompanion.Services
{
    public class GameItemService
    {
        readonly HttpClient httpClient;
        List<GameItem> gameItemList;

        public GameItemService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<GameItem>> GetGameItems()
        {
            if (gameItemList?.Count > 0)
                return gameItemList;

            var response = await httpClient.GetAsync("https://raw.githubusercontent.com/EXBO-Studio/stalcraft-database/main/ru/listing.json");
            if (response.IsSuccessStatusCode)
            {
                gameItemList = await response.Content.ReadFromJsonAsync(GameItemContext.Default.ListGameItem);
            }

            return gameItemList;
        }
    }
}
