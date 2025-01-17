﻿using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace RandomG
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
                    fonts.AddFont("AsusRog-Regular.ttf", "AsusRogRegular");
                    fonts.AddFont("SOV_KhianKhao.ttf", "SOVKhianKhao");
                });

            builder.Services.AddSingleton<IAudioManager, AudioManager>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
