using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SnakeMobile.Database;
using SnakeMobile.ViewModels;

namespace SnakeMobile
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

            builder.Services.AddSingleton<IUserRepo, UserRepo>();  // Register the UserRepo
            builder.Services.AddSingleton<LoginPage>();           // Register the LoginPage
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<RegisterPage>();

            // Register the DatabaseService
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "SnakeGame.db3");
            builder.Services.AddSingleton(new DatabaseService(dbPath)); // Add the DatabaseService with the database path

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
