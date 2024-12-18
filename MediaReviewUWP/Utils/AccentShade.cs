using MediaReviewUWP.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace MediaReviewUWP.Settings
{
    public class AccentShade : INotifyPropertyChanged
    {
        public AccentColor AccentName { get; set; }

        private SolidColorBrush _mainShade;
        public SolidColorBrush MainShade
        {
            get => _mainShade;
            set
            {
                if (_mainShade != value)
                {
                    _mainShade = value;
                    OnPropertyChanged(nameof(MainShade));
                }
            }
        }

        private SolidColorBrush _darkShade1;
        public SolidColorBrush DarkShade1
        {
            get => _darkShade1;
            set
            {
                if (_darkShade1 != value)
                {
                    _darkShade1 = value;
                    OnPropertyChanged(nameof(DarkShade1));
                }
            }
        }

        private SolidColorBrush _darkShade2;
        public SolidColorBrush DarkShade2
        {
            get => _darkShade2;
            set
            {
                if (_darkShade2 != value)
                {
                    _darkShade2 = value;
                    OnPropertyChanged(nameof(DarkShade2));
                }
            }
        }

        private SolidColorBrush _darkShade3;
        public SolidColorBrush DarkShade3
        {
            get => _darkShade3;
            set
            {
                if (_darkShade3 != value)
                {
                    _darkShade3 = value;
                    OnPropertyChanged(nameof(DarkShade3));
                }
            }
        }

        private SolidColorBrush _lightShade1;
        public SolidColorBrush LightShade1
        {
            get => _lightShade1;
            set
            {
                if (_lightShade1 != value)
                {
                    _lightShade1 = value;
                    OnPropertyChanged(nameof(LightShade1));
                }
            }
        }

        private SolidColorBrush _lightShade2;
        public SolidColorBrush LightShade2
        {
            get => _lightShade2;
            set
            {
                if (_lightShade2 != value)
                {
                    _lightShade2 = value;
                    OnPropertyChanged(nameof(LightShade2));
                }
            }
        }

        private SolidColorBrush _lightShade3;
        public SolidColorBrush LightShade3
        {
            get => _lightShade3;
            set
            {
                if (_lightShade3 != value)
                {
                    _lightShade3 = value;
                    OnPropertyChanged(nameof(LightShade3));
                }
            }
        }

        public AccentShade(Accent theme)
        {
            if (theme != null)
            {
                AccentName = theme.ThemeColor;
                MainShade = theme.MainShade;
                DarkShade1 = theme.DarkShade1;
                DarkShade2 = theme.DarkShade2;
                DarkShade3 = theme.DarkShade3;
                LightShade1 = theme.LightShade1;
                LightShade2 = theme.LightShade2;
                LightShade3 = theme.LightShade3;
            }
        }

        public AccentShade() : this(AccentManager.GetAccentShade())
        {
        }

        public AccentShade(AccentShade accentShade)
        {
            if (accentShade != null)
            {
                AccentName = accentShade.AccentName;
                MainShade = accentShade.MainShade;
                DarkShade1 = accentShade.DarkShade1;
                DarkShade2 = accentShade.DarkShade2;
                DarkShade3 = accentShade.DarkShade3;
                LightShade1 = accentShade.LightShade1;
                LightShade2 = accentShade.LightShade2;
                LightShade3 = accentShade.LightShade3;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
