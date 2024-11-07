using Microsoft.Extensions.Logging;
using ZelekWieclaw.VisualProgrammingProject.BL;


namespace VisualProgrammingProject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var cs = new CatalogService();

            cs.GetAllBeerProducts();

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

            return builder.Build();
        }
    }
}