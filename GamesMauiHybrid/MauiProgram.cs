using BlazingBooks.Mobile.Services;
using BlazingBooks.Shared.Interfaces;
using BlazorApp1.Clients;
using CommunityToolkit.Maui;
using GamesBlazor.Shared.States;
using GamesRazorClasslibrary;
using Microsoft.Extensions.Logging;

namespace GamesMauiHybrid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            InteractiveRenderSettings.ConfigureBlazorHybridRenderModes();
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
                .UseMauiCommunityToolkit();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            var ApiUrl = "https://gamesapi1.azurewebsites.net/";
            //var ApiUrl = "http://10.0.2.2:5138/";
            builder.Services.AddHttpClient("https", client => client.BaseAddress = new Uri(ApiUrl!));
            builder.Services.AddSingleton<GamesClient>();
            builder.Services.AddSingleton<GenresClient>();
            builder.Services.AddSingleton<ICommonService, CommonService>();
            builder.Services.AddSingleton<IConnectivity>(c => Connectivity.Current);
            builder.Services.AddSingleton<ActivityIndicatorState>();

            return builder.Build();
        }
    }
}
