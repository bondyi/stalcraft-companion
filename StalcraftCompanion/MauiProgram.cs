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
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton(Connectivity.Current);

        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton<GameItemService>();
        builder.Services.AddSingleton<GameItemsViewModel>();
		builder.Services.AddSingleton<DatabasePage>();

        builder.Services.AddTransient<GameItemDetailsViewModel>();
        builder.Services.AddTransient<DetailsPage>();

        return builder.Build();
	}
}
