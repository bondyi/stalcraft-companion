using CommunityToolkit.Mvvm.ComponentModel;

namespace StalcraftCompanion.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;

        protected readonly IConnectivity connectivity;

        public BaseViewModel(IConnectivity connectivity)
        {
            this.connectivity = connectivity;
        }
        
        protected bool IsInternetAvailable()
        {
            return connectivity.NetworkAccess != NetworkAccess.Internet;
        }
    }
}
