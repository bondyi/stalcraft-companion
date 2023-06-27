using StalcraftCompanion.ViewModel;

namespace StalcraftCompanion.View;

public partial class DatabasePage : ContentPage
{
	public DatabasePage(GameItemsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		var viewModel = (GameItemsViewModel)BindingContext;

		if (viewModel.IsInit) return;

		await viewModel.GetGameItemsAsync();
    }
}