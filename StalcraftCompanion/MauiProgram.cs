using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using StalcraftCompanion.Services;
using StalcraftCompanion.View;
using StalcraftCompanion.ViewModel;

namespace StalcraftCompanion;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton(Connectivity.Current);

		builder.Services.AddSingleton<ApiService>();

		builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<HomePage>();

        builder.Services.AddSingleton<GameItemService>();
        builder.Services.AddSingleton<DatabaseViewModel>();
		builder.Services.AddSingleton<DatabasePage>();

        builder.Services.AddTransient<DetailsViewModel>();
        builder.Services.AddTransient<DetailsPage>();

        builder.Services.AddSingleton<AuthService>();
        builder.Services.AddSingleton<ProfileViewModel>();
        builder.Services.AddSingleton<ProfilePage>();

        return builder.Build();
	}
}
