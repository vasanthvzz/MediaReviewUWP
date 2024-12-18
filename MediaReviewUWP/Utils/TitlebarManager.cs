using System;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;

namespace MediaReviewUWP.Utils
{
    public static class TitlebarManager
    {
        public static void ChangeTitlebarTheme(ApplicationViewTitleBar titlebar)
        {
            var color = ThemeManager.CurrentElementTheme == ElementTheme.Dark ?
            (Color)Application.Current.Resources["MildDarkColor"] : (Color)Application.Current.Resources["MildLightColor"];
            titlebar.BackgroundColor = titlebar.ButtonBackgroundColor = titlebar.InactiveBackgroundColor = titlebar.ButtonInactiveBackgroundColor = color;
        }

        public static void ChangeTitlebarTheme(AppWindowTitleBar titlebar)
        {
            var color = ThemeManager.CurrentElementTheme == ElementTheme.Dark ?
            (Color)Application.Current.Resources["MildDarkColor"] : (Color)Application.Current.Resources["MildLightColor"];
            titlebar.BackgroundColor = titlebar.ButtonBackgroundColor = titlebar.InactiveBackgroundColor = titlebar.ButtonInactiveBackgroundColor = color;
        }
    }
}
