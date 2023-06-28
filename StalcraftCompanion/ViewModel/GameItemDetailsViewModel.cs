using CommunityToolkit.Mvvm.ComponentModel;
using StalcraftCompanion.Model;

namespace StalcraftCompanion.ViewModel
{
    [QueryProperty(nameof(GameItem), "GameItem")]
    public partial class GameItemDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        GameItem gameItem;
    }
}
