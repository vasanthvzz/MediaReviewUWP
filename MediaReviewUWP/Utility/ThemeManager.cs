using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Utility;
using System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;

namespace MediaReviewUWP.Utility
{
    public static class ThemeManager
    {
        public static ElementTheme CurrentElementTheme { get; private set; }
        public static Action ThemeChanged;

        static ThemeManager()
        {
            var theme = SessionManager.GetApplicationTheme();
            if (theme != null && string.Equals(theme, "dark", StringComparison.OrdinalIgnoreCase))
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
            ChangeWindowTheme(WindowManager.MainWindow);
            ChangeWindowTheme(WindowManager.SettingsWindow);
            ChangeWindowTheme(WindowManager.CreateMovieWindow);
            ThemeChanged?.Invoke();
            SessionManager.SetApplicationTheme(CurrentElementTheme.ToString());
        }

        private static void ChangeWindowTheme(Window window)
        {
            if (window != null && window.Content is FrameworkElement mainRootElement)
            {
                mainRootElement.RequestedTheme = CurrentElementTheme;
                var mainWindowTitleBar = ApplicationView.GetForCurrentView().TitleBar;
                TitleBarManager.ChangeTitleBarTheme(mainWindowTitleBar);
            }
        }

        private static void ChangeWindowTheme(AppWindow window)
        {
            if (window != null)
            {
                var appWindowTitleBar = window.TitleBar;
                TitleBarManager.ChangeTitleBarTheme(appWindowTitleBar);
                if(window.Content is UIContentRoot element)
                {
                    
                }
                if (ElementCompositionPreview.GetAppWindowContent(window) is Frame contentFrame)
                {
                    contentFrame.RequestedTheme = CurrentElementTheme;
                }
            }
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