using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StalcraftCompanion.Model;
using StalcraftCompanion.Services;
using StalcraftCompanion.View;
using System.Collections.ObjectModel;

namespace StalcraftCompanion.ViewModel
{
    public partial class GameItemsViewModel : BaseViewModel
    {
        private const string GITHUB_URL = "https://raw.githubusercontent.com/EXBO-Studio/stalcraft-database/main/ru";

        public ObservableCollection<GameItem> ObservableGameItems { get; } = new();
        private List<GameItem> gameItems = new();

        readonly GameItemService gameItemService;
        readonly IConnectivity connectivity;

        [ObservableProperty]
        bool isRefreshing;

        public bool IsInit { get; private set; }

        public GameItemsViewModel(GameItemService gameItemService, IConnectivity connectivity)
        {
            this.gameItemService = gameItemService;
            this.connectivity = connectivity;
            IsInit = false;
        }

        [RelayCommand]
        async Task GoToDetails(GameItem gameItem)
        {
            if (gameItem == null)
                return;

            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object> {{"GameItem", gameItem }});
        }

        [RelayCommand]
        public async Task GetGameItemsAsync()
        {
            if (IsBusy) return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Error", "Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;

                gameItems = await gameItemService.GetGameItems();

                if (!IsInit)
                {
                    foreach (var gameItem in gameItems)
                    {
                        gameItem.Data = GITHUB_URL + gameItem.Data;
                        gameItem.Icon = GITHUB_URL + gameItem.Icon;
                    }
                }

                AddGameItems();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsInit = true;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async void RefreshAsync()
        {
            ObservableGameItems.Clear();
            await GetGameItemsAsync();
        }

        [RelayCommand]
        void LoadMore()
        {
            AddGameItems();
        }

        private void AddGameItems()
        {
            if (ObservableGameItems.Count == gameItems.Count) return;

            ObservableGameItems.Add(gameItems[ObservableGameItems.Count]);
        }

    }
}
