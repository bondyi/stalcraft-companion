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

        public GameItemsViewModel(GameItemService gameItemService, IConnectivity connectivity)
        {
            this.gameItemService = gameItemService;
            this.connectivity = connectivity;
        }

        [RelayCommand]
        async Task GoToDetails(GameItem gameItem)
        {
            if (gameItem == null)
                return;

            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object> {{"GameItem", gameItem }});
        }

        [RelayCommand]
        async Task GetGameItemsAsync()
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

                if (ObservableGameItems.Count != 0) ObservableGameItems.Clear();

                AddGameItems();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        void LoadMore()
        {
            AddGameItems();
        }

        private void AddGameItems()
        {
            var count = ObservableGameItems.Count;

            for (var i = count; i < count + 20; ++i)
            {
                gameItems[i].Data = GITHUB_URL + gameItems[i].Data;
                gameItems[i].Icon = GITHUB_URL + gameItems[i].Icon;
                ObservableGameItems.Add(gameItems[i]);
            }
        }

    }
}
