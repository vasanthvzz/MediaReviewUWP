using MediaReviewUWP.Utils;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MediaReviewUWP.View.SettingsView
{
    public sealed partial class SettingsPage : Page
    {
        public event EventHandler<ThemeChangeEventArgs> ThemeChangeEvent;

        public SettingsPage()
        {
            this.InitializeComponent();
            ThemeManager.ThemeChanged -= ThemeManager_ThemeChanged;
            ThemeManager.ThemeChanged += ThemeManager_ThemeChanged;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAccentColor();
            GetTheme();
        }

        private void ThemeManager_ThemeChanged()
        {
            GetTheme();
        }

        private void GetTheme()
        {
            ElementTheme theme = ThemeManager.CurrentElementTheme;
            if(theme == ElementTheme.Dark)
            {
                ThemeChangeListBox.SelectedIndex = 0;            }
            else
            {
                ThemeChangeListBox.SelectedIndex = 1;
            }
        }

        private void GetAccentColor()
        {
            var themeSettings = AccentManager.GetAccentShade();
            switch (themeSettings.AccentName)
            {
                case AccentColor.ORANGE:
                    {
                        AccentListBox.SelectedIndex = 0;
                        break;
                    }
                case AccentColor.GREEN:
                    {
                        AccentListBox.SelectedIndex = 1;
                        break;
                    }
                case AccentColor.BLUE:
                    {
                        AccentListBox.SelectedIndex = 2;
                        break;
                    }
                default:
                    break;
            }
        }

        public void UpdateAccentColor(AccentColor themeColor)
        {
            AccentManager.AccentChangeRequest(themeColor);
        }        

        private void AccentListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var themeColor = AccentColor.BLUE;
            var index = AccentListBox.SelectedIndex;
            UpdateListBoxContent(index);
            switch (index)
            {
                case 0:
                    {
                        themeColor = AccentColor.ORANGE;
                        break;
                    }
                case 1:
                    {
                        themeColor = AccentColor.GREEN;
                        break;
                    }
                case 2:
                    {
                        themeColor = AccentColor.BLUE;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            UpdateAccentColor(themeColor);
        }

        private void UpdateListBoxContent(int index)
        {
            CollapseAllTicks();
            switch (index)
            {
                case 0:
                    {
                        OrangeTickIcon.Visibility = Visibility.Visible;
                        break;
                    }
                case 1:
                    {
                        GreenTickIcon.Visibility = Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        BlueTickIcon.Visibility = Visibility.Visible;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void CollapseAllTicks()
        {
            GreenTickIcon.Visibility = Visibility.Collapsed;
            BlueTickIcon.Visibility = Visibility.Collapsed;
            OrangeTickIcon.Visibility = Visibility.Collapsed;
        }

        private void ThemeChangeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded)
            {
                return;
            }
            ElementTheme theme = ElementTheme.Dark;
            if(ThemeChangeListBox.SelectedIndex == 0)
            {
                theme = ElementTheme.Dark;
            }
            else
            {
                theme = ElementTheme.Light;
            }
            ThemeManager.RequestThemeChange(theme);
        }

        private void LayoutListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            return;
        }
    }

    public class ThemeChangeEventArgs : EventArgs
    {
        public AccentColor ThemeColor { get; set; }

        public ThemeChangeEventArgs(AccentColor themeColor) 
        {
            ThemeColor = themeColor;
        }
    }
}
