using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StalcraftCompanion.Model;
using StalcraftCompanion.Services;
using StalcraftCompanion.View;
using System.Collections.ObjectModel;

namespace StalcraftCompanion.ViewModel
{
    public partial class DatabaseViewModel : BaseViewModel
    {
        public ObservableCollection<GameItem> ObservableGameItems { get; } = new();
        private List<GameItem> gameItems = new();

        readonly GameItemService gameItemService;

        [ObservableProperty]
        bool isRefreshing;

        public DatabaseViewModel(GameItemService gameItemService, IConnectivity connectivity) 
            : base(connectivity)
        {
            this.gameItemService = gameItemService;
        }

        [RelayCommand]
        async Task GoToDetails(string itemUrl)
        {
            if (itemUrl == null) return;

            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object> {{"itemUrl", itemUrl }});
        }

        [RelayCommand]
        public async Task GetGameItemsAsync()
        {
            if (IsBusy) return;

            try
            {
                if (IsInternetAvailable())
                {
                    await Shell.Current.DisplayAlert("Error", "Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;

                gameItems = await gameItemService.GetGameItems();

                LoadMore();
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
        async void RefreshAsync()
        {
            ObservableGameItems.Clear();
            await GetGameItemsAsync();
        }

        [RelayCommand]
        void LoadMore()
        {
            for (var i = 0; i < 11; ++i)
            {
                AddGameItem();
            }
        }

        private void AddGameItem()
        {
            if (ObservableGameItems.Count == gameItems.Count) return;

            ObservableGameItems.Add(gameItems[ObservableGameItems.Count]);
        }

    }
}
