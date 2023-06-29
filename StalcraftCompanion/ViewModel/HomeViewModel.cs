using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StalcraftCompanion.Model;
using StalcraftCompanion.Services;

namespace StalcraftCompanion.ViewModel
{
    public partial class HomeViewModel : BaseViewModel
    {
        readonly ApiService service;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        Emission emission;

        [ObservableProperty]
        bool isEmissionActive;

        public bool IsEmissionInactive => !IsEmissionActive;

        public HomeViewModel(ApiService service, IConnectivity connectivity) 
            : base(connectivity)
        {
            this.service = service;
        }

        public async Task GetEmissionStatusAsync()
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

                Emission = await service.GetEmissionStatus();

                IsEmissionActive = Emission.CurrentStart != null;
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
            await GetEmissionStatusAsync();
        }
    }
}
