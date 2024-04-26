using MauiApp1.Model;
using MauiApp1.ViewModel;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MauiApp1
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

            int userId = 1;

            builder.Services.AddSingleton<Repository>(serviceProvider =>
            {
                string localPath = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                string usersFilePath = localPath + @"\Data\users.xml";
                string chatsFilePath = localPath + @"\Data\chats.xml";
                return new Repository(usersFilePath, chatsFilePath);
            });

            builder.Services.AddSingleton<Service>(serviceProvider =>
            {
                var repo = serviceProvider.GetRequiredService<Repository>();
                return new Service(repo);
            });

            builder.Services.AddTransient<MainPageViewModel>(serviceProvider =>
            {
                var service = serviceProvider.GetRequiredService<Service>();
                return new MainPageViewModel(service, userId);
            });

            builder.Services.AddTransient<MainPage>(serviceProvider =>
            {
                var viewModel = serviceProvider.GetRequiredService<MainPageViewModel>();
                return new MainPage(viewModel);
            });

            builder.Services.AddTransient<ChatPageViewModel>(serviceProvider =>
            {
                var service = serviceProvider.GetRequiredService<Service>();
                return new ChatPageViewModel(service, userId);
            });

            builder.Services.AddTransient<ChatPage>(serviceProvider =>
            {
                var viewModel = serviceProvider.GetRequiredService<ChatPageViewModel>();
                return new ChatPage(viewModel);
            });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
