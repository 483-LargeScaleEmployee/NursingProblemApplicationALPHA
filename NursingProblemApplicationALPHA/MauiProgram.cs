using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui;

namespace NursingProblemApplicationALPHA;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
        .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        })
        .UseMauiCommunityToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<SeniorPage>();
        builder.Services.AddSingleton<MainViewModel>();
        return builder.Build();
    }
}