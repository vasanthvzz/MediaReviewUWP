using MediaReviewUWP.Utility;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace MediaReviewUWP.View.SettingsView
{
    public sealed partial class SettingsPage : Page
    {
        public event EventHandler<ThemeChangeEventArgs> ThemeChangeEvent;

        public static event Action LanguageChanged;

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
            if (theme == ElementTheme.Dark)
            {
                ThemeChangeListBox.SelectedIndex = 0;
            }
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
            }
            UpdateAccentColor(themeColor);
        }

        private void MyComboBox_DropDownOpened(object sender, object e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                var grid = VisualTreeHelper.GetChild(comboBox, 0) as Grid;
                var popup = VisualTreeHelper.GetChild(grid,7) as Popup;
                if (popup != null)
                {
                    popup.RequestedTheme = ElementTheme.Dark; // Change as per your app theme
                }
            }
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
            if (ThemeChangeListBox.SelectedIndex == 0)
            {
                theme = ElementTheme.Dark;
            }
            else
            {
                theme = ElementTheme.Light;
            }
            ThemeManager.RequestThemeChange(theme);
        }

        //private void LanguageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (!LanguageBox.IsLoaded) return;

        //    string presentLanguage = ApplicationLanguages.PrimaryLanguageOverride;
        //    string selectedLanguage = "";
        //    switch (LanguageBox.SelectedIndex)
        //    {
        //        case 0:
        //            selectedLanguage = "en";
        //            break;

        //        case 1:
        //            selectedLanguage = "ta";
        //            break;

        //        default:
        //            selectedLanguage = presentLanguage;
        //            break;
        //    }

        //    if (selectedLanguage != presentLanguage)
        //    {
        //        ApplicationLanguages.PrimaryLanguageOverride = selectedLanguage;
        //        _ = CoreApplication.RequestRestartAsync("");
        //    }
        //}

        //private void UpdateLangageBox()
        //{
        //    int index = 0;
        //    switch (ApplicationLanguages.PrimaryLanguageOverride)
        //    {
        //        case "en":
        //            {
        //                index = 0;
        //                break;
        //            }
        //        case "ta":
        //            {
        //                index = 1;
        //                break;
        //            }
        //    }
        //    LanguageBox.SelectedIndex = index;
        //}

        //private void LanguageBox_Loaded(object sender, RoutedEventArgs e)
        //{
        //    UpdateLangageBox();
        //}

        private void LanguageSplitButton_Loaded(object sender, RoutedEventArgs e)
        {
            string language = "English";
            switch (ApplicationLanguages.PrimaryLanguageOverride)
            {
                case "en":
                    {
                        language = "English";
                        break;
                    }
                case "ta":
                    {
                        language = "தமிழ்";
                        break;
                    }
            }
            SelectedLanguageText.Text = language;
        }

        private void LanguageOption_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem selectedItem)
            {
                SelectedLanguageText.Text = selectedItem.Text;
                string presentLanguage = ApplicationLanguages.PrimaryLanguageOverride;
                string selectedLanguage = selectedItem.Tag.ToString();
                if (selectedLanguage != presentLanguage)
                {
                    ApplicationLanguages.PrimaryLanguageOverride = selectedLanguage;
                    _ = CoreApplication.RequestRestartAsync("");
                }
            }
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