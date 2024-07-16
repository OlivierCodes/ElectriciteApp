using ElectriciteApp.Data;
using ElectriciteApp.Views;
using Microsoft.Extensions.Logging;

namespace ElectriciteApp
{
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

            builder.Services.AddSingleton<ListeDesLocataires>();
            builder.Services.AddTransient<ListeDesLocatairesItemPage>();
            builder.Services.AddSingleton<Database>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif


            //Services



            return builder.Build();
        }
    }
}
