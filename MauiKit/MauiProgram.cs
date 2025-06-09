using CommunityToolkit.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Microsoft.Maui.LifecycleEvents;
using MauiKit.Handlers;
using CommunityToolkit.Maui.Core;
using Plugin.Firebase.Analytics;
using Plugin.Firebase.CloudMessaging;
using Plugin.Firebase.Auth.Google;
using Microsoft.Extensions.Logging;
using PanCardView;
using FFImageLoading.Maui;
using RGPopup.Maui.Extensions;
using MauiKit.Views;
using MauiMaps;
//using MauiKit.Views.Onboardings;
using Microsoft.Maui.Controls.Hosting;
using MyMauiApp.CustomHandlers;
using MauiKit.Views.DemoApp;
using MauiKit.Services;
#if ANDROID



#endif



#if IOS
using Plugin.Firebase.Core.Platforms.iOS;
using UIKit;
#elif ANDROID
using Plugin.Firebase.Core.Platforms.Android;
#endif



#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using WinRT.Interop;
using Windows.UI.Core;
using Plugin.Firebase.CloudMessaging;

#endif

namespace MauiKit
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
           

            var builder = MauiApp.CreateBuilder();

            EntryCustomHandler.RemoveEntryBorder();

            builder
                .UseMauiApp<App>()
                .UseMauiMaps()
                .RegisterDemoAppServices()
                //.RegisterViewModels()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .UseSkiaSharp()
                .UseCardsView()
                .UseFFImageLoading()
                .ConfigureMauiHandlers(handlers =>
                {
#if IOS || ANDROID
                    handlers.AddHandler<Microsoft.Maui.Controls.Maps.Map, CustomMapHandler>();
#endif
                })
                .UseMauiRGPopup(config =>
                {
                    config.BackPressHandler = null;
                    config.FixKeyboardOverlap = true;
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Poppins-Regular.otf", "RegularFont");
                    fonts.AddFont("Poppins-Medium.otf", "MediumFont");
                    fonts.AddFont("Poppins-SemiBold.otf", "SemiBoldFont");
                    fonts.AddFont("Poppins-Bold.otf", "BoldFont");
                    fonts.AddFont("Caveat-Bold.ttf", "HandwritingBoldFont");
                    fonts.AddFont("Caveat-Regular.ttf", "HandwritingFont");

                    fonts.AddFont("fa-solid-900.ttf", "FaPro");
                    fonts.AddFont("fa-brands-400.ttf", "FaBrands");
                    fonts.AddFont("fa-regular-400.ttf", "FaRegular");
                    fonts.AddFont("line-awesome.ttf", "LineAwesome");
                    fonts.AddFont("material-icons-outlined-regular.otf", "MaterialDesign");
                    fonts.AddFont("ionicons.ttf", "IonIcons");
                    fonts.AddFont("icon.ttf", "MauiKitIcons");
                })
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler(typeof(ProgressBar), typeof(ProgressBarHandler));
                })
                .ConfigureLifecycleEvents(events =>
                {
#if ANDROID
                    events.AddAndroid(android => android.OnCreate((activity, bundle) => MakeStatusBarTranslucent(activity)));

                    static void MakeStatusBarTranslucent(Android.App.Activity activity)
                    {
                        //activity.Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);

                        activity.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);

                        activity.Window.SetStatusBarColor(Android.Graphics.Color.White);

                        activity.Window.SetNavigationBarColor(Android.Graphics.Color.Transparent);
                    }
#endif
                });

#if DEBUG
            builder.Logging.AddDebug();
            builder.Services.AddLogging(configure => configure.AddDebug());
#endif

            builder.ConfigureLifecycleEvents(events =>
            {
#if WINDOWS
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
                       
                        //https://github.com/dotnet/maui/issues/7751
                        window.ExtendsContentIntoTitleBar = false; // must be false or else you see some of the buttons
                        winuiAppWindow.SetPresenter(AppWindowPresenterKind.Default);

                        //https://github.com/microsoft/microsoft-ui-xaml/issues/8746
                        ///
                        /// System back button for backwards navigation is no longer recommend
                        /// Instead, you should provide your own in-app back button
                        //  https://learn.microsoft.com/en-us/windows/apps/design/basics/navigation-history-and-backwards-navigation?source=recommendations#system-back-behavior-for-backward-compatibility
                        var titleBar = winuiAppWindow.TitleBar;
                        titleBar.ExtendsContentIntoTitleBar = true;
                        titleBar.BackgroundColor = Windows.UI.Color.FromArgb(1, 186, 213, 248); //Hex: #BAD5F8
                        titleBar.ButtonBackgroundColor = Windows.UI.Color.FromArgb(1, 186, 213, 248); //Hex: #BAD5F8
                        titleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Black;
                        titleBar.ButtonHoverBackgroundColor = Windows.UI.Color.FromArgb(1, 186, 213, 248); //Hex: #BAD5F8
                        titleBar.ForegroundColor = Microsoft.UI.Colors.White;
                        titleBar.InactiveBackgroundColor = Microsoft.UI.Colors.Black;

                        //https://github.com/dotnet/maui/issues/6976
                        var displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(win32WindowsId, Microsoft.UI.Windowing.DisplayAreaFallback.Nearest);
                        
                        int width = displayArea.WorkArea.Width * 3 / 4;
                        int height = displayArea.WorkArea.Height - 50;

                        winuiAppWindow.MoveAndResize(new RectInt32(25, 50, width, height));

                    });
                });
#endif
            });

            builder.Services.AddLocalization();

            builder.Services.AddSingleton<LocationUpdaterService>();

            // Bắt lifecycle: khi app chạy, khởi động định vị nền
            builder.ConfigureLifecycleEvents(events =>
            {
#if ANDROID
            events.AddAndroid(android => android
                .OnStart((activity) =>
                {
                    var updater = new LocationUpdaterService();
                    Device.StartTimer(TimeSpan.FromMinutes(1), () =>
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await updater.UpdateLocationAsync();
                        });

                        return true;
                    });
                }));
#endif

#if IOS
            events.AddiOS(ios => ios
                .FinishedLaunching((app, dict) =>
                {
                    var updater = new LocationUpdaterService();
                    Device.StartTimer(TimeSpan.FromMinutes(1), () =>
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await updater.UpdateLocationAsync();
                        });

                        return true;
                    });

                    return true;
                }));
#endif
            });


            var app = builder.Build();

            return app;
        }

        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
#if IOS
                events.AddiOS(iOS => iOS.WillFinishLaunching((app, launchOptions) =>
                {
                    CrossFirebase.Initialize();
                    FirebaseAuthGoogleImplementation.Initialize();
                    FirebaseCloudMessagingImplementation.Initialize();
                    return true;
                }));
#elif ANDROID
                events.AddAndroid(android => android.OnCreate((activity, _) =>
                {
                    CrossFirebase.Initialize(activity);
                    FirebaseAnalyticsImplementation.Initialize(activity);
                    FirebaseAuthGoogleImplementation.Initialize("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"); /*webclient findmychildrens project*/
                }));
#endif
            });

            CrossFirebaseCloudMessaging.Current.NotificationTapped += Current_NotificationTapped;
            return builder;
        }

        public static MauiAppBuilder RegisterDemoAppServices(this MauiAppBuilder mauiAppBuilder)
        {
            return mauiAppBuilder;
        }
        private static void Current_NotificationTapped(object? sender, Plugin.Firebase.CloudMessaging.EventArgs.FCMNotificationTappedEventArgs e)
        {
            var notificationData = e.Notification.Data;
            if (notificationData.ContainsKey("link"))
            {
                //Do something with the link
            }
        }
        //public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        //{
        //    mauiAppBuilder.Services.AddTransient<MainPage>();
        //    mauiAppBuilder.Services.AddTransient<ProfileUser>();
        //    mauiAppBuilder.Services.AddTransient<TestPageViewModel>();
        //    mauiAppBuilder.Services.AddTransient<MainViewModel>();
        //    mauiAppBuilder.Services.AddTransient<DemoWalkthroughViewModel>();
        //    mauiAppBuilder.Services.AddTransient<DemoStartPage>();
        //    mauiAppBuilder.Services.AddTransient<DemoWalkthroughPage>();

        //    return mauiAppBuilder;
        //}
    }
}