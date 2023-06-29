using CommunityToolkit.Mvvm.ComponentModel;
using StalcraftCompanion.Model;
using StalcraftCompanion.Services;

namespace StalcraftCompanion.ViewModel
{
    [QueryProperty(nameof(ItemUrl), "itemUrl")]
    public partial class DetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        GameItemInfo gameItemInfo;

        readonly GameItemService service;

        private string itemUrl;
        public string ItemUrl
        {
            get => itemUrl;
            set => SetProperty(ref itemUrl, value);
        }

        public DetailsViewModel(GameItemService service, IConnectivity connectivity) 
            : base(connectivity)
        {
            this.service = service;
        }

        public async Task GetGameItemInfoAsync()
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

                GameItemInfo = await service.GetGameItem(ItemUrl);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
