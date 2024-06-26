﻿using System.Reflection;
using MauiApp1.Model;
using MauiApp1.ViewModel;
using Microsoft.Extensions.Logging;

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

            builder.Services.AddHttpClient<ApiService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5086/");
            });

            builder.Services.AddSingleton<Service>(serviceProvider =>
            {
                var apiService = serviceProvider.GetRequiredService<ApiService>();
                return new Service(apiService);
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
