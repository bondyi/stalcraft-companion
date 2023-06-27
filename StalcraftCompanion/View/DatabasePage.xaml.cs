using StalcraftCompanion.ViewModel;

namespace StalcraftCompanion.View;

public partial class DatabasePage : ContentPage
{
	public DatabasePage(GameItemsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}