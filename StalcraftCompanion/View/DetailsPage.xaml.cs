using StalcraftCompanion.ViewModel;

namespace StalcraftCompanion.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var viewModel = (DetailsViewModel)BindingContext;

        await viewModel.GetGameItemInfoAsync();
    }
}