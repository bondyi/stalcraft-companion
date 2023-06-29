using StalcraftCompanion.ViewModel;

namespace StalcraftCompanion.View;

public partial class DatabasePage : ContentPage
{
	public DatabasePage(DatabaseViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		var viewModel = (DatabaseViewModel)BindingContext;

		await viewModel.GetGameItemsAsync();
    }
}