using MediaReviewUWP.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media;

namespace MediaReviewUWP.Settings
{
    public class ThemeSettings : INotifyPropertyChanged
    {
        public ThemeColor ThemeName { get; set; }

        private SolidColorBrush _mainShade;
        public SolidColorBrush MainShade
        {
            get => _mainShade;
            set
            {
                if (_mainShade != value)
                {
                    _mainShade = value;
                    OnPropertyChanged();
                }
            }
        }

        private SolidColorBrush _lightShade;
        public SolidColorBrush LightShade
        {
            get => _lightShade;
            set
            {
                if (_lightShade != value)
                {
                    _lightShade = value;
                    OnPropertyChanged();
                }
            }
        }

        private SolidColorBrush _darkShade;
        public SolidColorBrush DarkShade
        {
            get => _darkShade;
            set
            {
                if (_darkShade != value)
                {
                    _darkShade = value;
                    OnPropertyChanged();
                }
            }
        }

        private SolidColorBrush _darkestShade;
        private ThemeSettings themeSettings;

        public SolidColorBrush DarkestShade
        {
            get => _darkestShade;
            set
            {
                if (_darkestShade != value)
                {
                    _darkestShade = value;
                    OnPropertyChanged();
                }
            }
        }

        // Constructor that initializes properties using a Theme object
        public ThemeSettings(Theme theme)
        {
            if (theme != null)
            {
                ThemeName = theme.ThemeColor;
                MainShade = theme.MainShade;
                LightShade = theme.LightShade;
                DarkShade = theme.DarkShade;
                DarkestShade = theme.DarkestShade;
            }
        }

        public ThemeSettings() : this(ThemeManager.GetThemeSettings())
        {
        }

        public ThemeSettings(ThemeSettings themeSettings)
        {
            this.themeSettings = themeSettings;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
