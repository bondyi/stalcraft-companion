using StalcraftCompanion.ViewModel;

namespace StalcraftCompanion.View;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var viewModel = (HomeViewModel)BindingContext;

        await viewModel.GetEmissionStatusAsync();
    }
}

