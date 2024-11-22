using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace MediaReviewUWP.Utils
{
    public static class ThemeManager
    {
        private static ThemeSettings _themeSetting { get; set; }
        private static ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();

        static ThemeManager()
        {
            string color = _sessionManager.GetAccentColor();
            if (color == null || color == "blue")
            {
                _themeSetting = new ThemeSettings(ThemeRepo.GetBlueTheme());
            }
            else if (color == "orange")
            {
                _themeSetting = new ThemeSettings(ThemeRepo.GetOrangeTheme());
            }
            else if (color == "green")
            {
                _themeSetting = new ThemeSettings(ThemeRepo.GetGreenTheme());
            }
            else
            {
                _themeSetting = new ThemeSettings(ThemeRepo.GetBlueTheme());
            }
        }

        public static ThemeSettings GetThemeSettings()
        {
            return _themeSetting;
        }

        public static void ToggleTheme()
        {
            if (ThemeManager.GetThemeSettings().ThemeName == ThemeColor.BLUE)
            {
                ThemeManager.SetOrangeTheme();
            }
            else if (ThemeManager.GetThemeSettings().ThemeName == ThemeColor.ORANGE)
            {
                ThemeManager.SetGreenTheme();
            }
            else if (ThemeManager.GetThemeSettings().ThemeName == ThemeColor.GREEN)
            {
                ThemeManager.SetBlueTheme();
            }
            _sessionManager.StoreAccentColor(GetThemeName());
        }

        private static string GetThemeName()
        {
            return _themeSetting.ThemeName.ToString();
        }

        private static void SetBlueTheme()
        {
            ThemeSettingSetter(ThemeRepo.GetBlueTheme());
        }

        private static void SetGreenTheme()
        {
            ThemeSettingSetter(ThemeRepo.GetGreenTheme());
        }

        public static void SetOrangeTheme()
        {
            ThemeSettingSetter(ThemeRepo.GetOrangeTheme());
        }

        private static void ThemeSettingSetter(Theme theme)
        {
            _themeSetting.ThemeName = theme.ThemeColor;
            _themeSetting.DarkestShade = theme.DarkestShade;
            _themeSetting.DarkShade = theme.DarkShade;
            _themeSetting.MainShade = theme.MainShade;
            _themeSetting.LightShade = theme.LightShade;
        }
    }
}
