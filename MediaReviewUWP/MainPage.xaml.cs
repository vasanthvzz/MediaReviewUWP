using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.View.HomePageView;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.ApplicationModel.Core;
using MediaReviewUWP.Utils;
using System;
using MediaReviewUWP.View.LandingPageView;
using System.Threading.Tasks;

namespace MediaReviewUWP
{
    public sealed partial class MainPage : Page
    {
        private readonly ISessionManager _sessionManager;

        public MainPage()
        {
            this.InitializeComponent();
            _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeTheme();
            UpdateThemeInTitleBar();
            RedirectPage();
            //ThemeManager.ThemeChanged -= ThemeManager_ThemeChanged;
            //ThemeManager.ThemeChanged += ThemeManager_ThemeChanged;
            AccentManager.ChangeAccent -= AccentManager_AccentChanged;
            AccentManager.ChangeAccent += AccentManager_AccentChanged;
            LandingPage.LoginSuccess -= OnLoginSuccess;
            LandingPage.LoginSuccess += OnLoginSuccess;
        }

        private void ThemeManager_ThemeChanged(object sender, ThemeChangeEventArgs e)
        {
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = false;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            var color = e.ThemeColor == ElementTheme.Dark ?
                (Color)Application.Current.Resources["MildDarkColor"] : (Color)Application.Current.Resources["MildLightColor"];
            titleBar.BackgroundColor = titleBar.ButtonBackgroundColor = titleBar.InactiveBackgroundColor = titleBar.ButtonInactiveBackgroundColor = color;
        }

        private async void AccentManager_AccentChanged(object sender, AccentChangeEventArgs e)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                AccentManager.UpdateTheme(e.AccentColor);
            });
        }

        private void ThemeManager_ThemeChanged()
        {
            UpdateThemeInTitleBar();
        }
            
        private void UpdateThemeInTitleBar()
        {
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = false;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            var color = ThemeManager.CurrentElementTheme == ElementTheme.Dark ?
                (Color)Application.Current.Resources["MildDarkColor"] : (Color)Application.Current.Resources["MildLightColor"];
            titleBar.BackgroundColor = titleBar.ButtonBackgroundColor= titleBar.InactiveBackgroundColor = titleBar.ButtonInactiveBackgroundColor = color;
        }

        public void InitializeTheme()
        {
            ElementTheme theme = ThemeManager.CurrentElementTheme;
            SolidColorBrush mildBackgroundBrush = (SolidColorBrush)Application.Current.Resources["MildBackground"];
            ((FrameworkElement)Window.Current.Content).RequestedTheme = theme;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = mildBackgroundBrush.Color;
        }

        private void RedirectPage()
        {
            if (_sessionManager.RetriveUserFromStorage() != null)
            {
                OnLoginSuccess();
            }
            else
            {
                MainFrame.Navigate(typeof(LandingPage));
            }
        }

        private async void OnLoginSuccess()
        {
            await RedirectToHomePage();
        }

        private async Task RedirectToHomePage()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                MainFrame.Navigate(typeof(HomePage));
            });
        }
    }
}

