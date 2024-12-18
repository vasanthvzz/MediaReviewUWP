using CommunityToolkit.WinUI;
using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Utlis;
using System;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace MediaReviewUWP.Utils
{
    public static class ThemeManager
    {
        public static ElementTheme CurrentElementTheme { get; private set; }
        private static ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetRequiredService<ISessionManager>();
        public static Action ThemeChanged;

        static ThemeManager()
        {
            var theme =  _sessionManager.GetApplicationTheme();
            if(theme != null && theme.ToLower() == "dark")
            {
                CurrentElementTheme = ElementTheme.Dark;
            }
            else
            {
                CurrentElementTheme = ElementTheme.Light;
            }
        }
        
        public static void RequestThemeChange()
        {
            ToggleElementTheme();
        }
        
        public static void RequestThemeChange(ElementTheme theme)
        {
            if (CurrentElementTheme != theme)
            {
                CurrentElementTheme = theme;
                ChangeAllTheme();
            }
        }

        private static void ToggleElementTheme()
        {
            if (CurrentElementTheme == ElementTheme.Default || CurrentElementTheme == ElementTheme.Dark)
            {
                CurrentElementTheme = ElementTheme.Light;
            }
            else
            {
                CurrentElementTheme = ElementTheme.Dark;
            }
            ChangeAllTheme();
        }

        public static void UpdateElementTheme()
        {
            ThemeChanged?.Invoke();    
        }

        private static void ChangeAllTheme()
        {
            // Update the Main Window's theme
            var mainWindow = WindowManager.MainWindow;
            if (mainWindow != null && mainWindow.Content is FrameworkElement mainRootElement)
            {
                // Change the theme of the main window's content
                mainRootElement.RequestedTheme = CurrentElementTheme;

                // Update the TitleBar theme for the main window
                var mainWindowTitleBar = ApplicationView.GetForCurrentView().TitleBar;
                TitlebarManager.ChangeTitlebarTheme(mainWindowTitleBar);
            }

            // Update the Settings AppWindow's theme
            AppWindow appWindow = WindowManager.SettingsWindow;
            if (appWindow != null)
            {
                // Update the TitleBar theme for the AppWindow
                var appWindowTitleBar = appWindow.TitleBar;
                TitlebarManager.ChangeTitlebarTheme(appWindowTitleBar);

                // Update the theme of the AppWindow's content
                if (ElementCompositionPreview.GetAppWindowContent(appWindow) is Frame contentFrame)
                {
                    contentFrame.RequestedTheme = CurrentElementTheme;
                }
            }

            ThemeChanged?.Invoke();
            _sessionManager.SetApplicationTheme(CurrentElementTheme.ToString());
        }

    }

    public class ThemeChangeEventArgs
    {
        public ElementTheme ThemeColor { get; set; }

        public ThemeChangeEventArgs(ElementTheme themeColor)
        {
            ThemeColor = themeColor;
        }
    }
}
