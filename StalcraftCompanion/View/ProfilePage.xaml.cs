using StalcraftCompanion.ViewModel;

namespace StalcraftCompanion.View;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfileViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}