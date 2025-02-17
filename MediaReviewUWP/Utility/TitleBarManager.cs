using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;

namespace MediaReviewUWP.Utility
{
    public static class TitleBarManager
    {
        public static void ChangeTitleBarTheme(ApplicationViewTitleBar titleBar)
        {
            var themeName = ThemeManager.CurrentElementTheme.ToString();
            var mildColor = (Color)Application.Current.Resources["Mild" + themeName + "Color"];
            //var color = (Color)Application.Current.Resources[themeName + "Color"];
            titleBar.BackgroundColor = titleBar.ButtonBackgroundColor = titleBar.InactiveBackgroundColor = titleBar.ButtonInactiveBackgroundColor = mildColor;
            //titleBar.ButtonHoverBackgroundColor = mildColor;
        }

        public static void ChangeTitleBarTheme(AppWindowTitleBar titleBar)
        {
            var themeName = ThemeManager.CurrentElementTheme.ToString();
            var mildColor = (Color)Application.Current.Resources["Mild" + themeName + "Color"];
            //var color = (Color)Application.Current.Resources[themeName + "Color"];
            titleBar.BackgroundColor = titleBar.ButtonBackgroundColor = titleBar.InactiveBackgroundColor = titleBar.ButtonInactiveBackgroundColor = mildColor;
            //titleBar.ButtonHoverBackgroundColor = mildColor;
        }
    }
}