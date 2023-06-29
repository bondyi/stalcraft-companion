using CommunityToolkit.Mvvm.Input;
using StalcraftCompanion.Services;

namespace StalcraftCompanion.ViewModel
{
    public partial class ProfileViewModel : BaseViewModel
    {
        readonly AuthService service;

        public ProfileViewModel(AuthService service, IConnectivity connectivity) 
            : base(connectivity)
        {
            this.service = service;
        }

        [RelayCommand]
        public void Login()
        {
            service.GetUserTokens();
        }

        [RelayCommand]
        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
