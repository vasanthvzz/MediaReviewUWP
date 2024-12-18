using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MediaReviewUWP.Utils
{
    public static class AccentManager
    {
        private static AccentShade _accentShades { get; set; }
        private static ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();
        public static EventHandler<AccentChangeEventArgs> ChangeAccent;

        static AccentManager()
        {
            string color = _sessionManager.GetAccentColor();
            switch (color)
            {
                case "blue":
                    {
                        _accentShades = new AccentShade(AccentRepo.GetBlueTheme());
                        break;
                    }

                case "orange":
                    {
                        _accentShades = new AccentShade(AccentRepo.GetOrangeTheme());
                        break;
                    }

                case "green":
                    {
                        _accentShades = new AccentShade(AccentRepo.GetGreenTheme());
                        break;
                    }
                default:
                    {
                        _accentShades = new AccentShade(AccentRepo.GetBlueTheme());
                        break;
                    }
            }
        }

        public static void InvokeChangeAccent(AccentColor color)
        {
            ChangeAccent.Invoke(null,new AccentChangeEventArgs(color));
        }

        public static AccentShade GetAccentShade()
        {
            return _accentShades;
        }

        private static void SetBlueTheme()
        {
            ThemeSettingSetter(AccentRepo.GetBlueTheme());
        }

        private static void SetGreenTheme()
        {
            ThemeSettingSetter(AccentRepo.GetGreenTheme());
        }

        public static void SetOrangeTheme()
        {
            ThemeSettingSetter(AccentRepo.GetOrangeTheme());
        }

        private static void ThemeSettingSetter(Accent theme)
        {
            _accentShades.AccentName = theme.ThemeColor;
            _accentShades.MainShade = theme.MainShade;
            _accentShades.DarkShade1 = theme.DarkShade1;
            _accentShades.DarkShade2 = theme.DarkShade2;
            _accentShades.DarkShade3 = theme.DarkShade3;
            _accentShades.LightShade1 = theme.LightShade1;
            _accentShades.LightShade2 = theme.LightShade2;
            _accentShades.LightShade3 = theme.LightShade3;
        }

        public static void AccentChangeRequest(AccentColor color)
        {
            InvokeChangeAccent(color);
        }

        public static void UpdateTheme(AccentColor themeColor)
        {
            switch (themeColor)
            {
                case AccentColor.ORANGE:
                    {
                        SetOrangeTheme();
                        break;
                    }
                case AccentColor.BLUE:
                    {
                        SetBlueTheme();
                        break;
                    }
                case AccentColor.GREEN:
                    {
                        SetGreenTheme();    
                        break;
                    }
                default:
                    {
                        throw new Exception("Invalid color option");
                    }
            }
            _sessionManager.StoreAccentColor(themeColor.ToString());
        }
    }

    public class AccentChangeEventArgs
    {
        public AccentColor AccentColor { get; set; }

        public AccentChangeEventArgs( AccentColor accentColor)
        {
            AccentColor = accentColor;
        }
    }
}
