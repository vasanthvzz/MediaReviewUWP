using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.View.HomePageView;
using MediaReviewUWP.View.WelcomePageView;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.ApplicationModel.Core;
using MediaReviewUWP.Utils;

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
            ThemeManager.ThemeChanged += ThemeManager_ThemeChanged;
        }

        private void ThemeManager_ThemeChanged()
        {
            UpdateThemeInTitleBar();
        }
            
        private void UpdateThemeInTitleBar()
        {
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = false;
            //Window.Current.UpdateThemeInTitleBar(AppTitleBar);
           
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            var color = ((FrameworkElement)Window.Current.Content).RequestedTheme == ElementTheme.Dark ?
                (Color)Application.Current.Resources["MildDarkColor"] : (Color)Application.Current.Resources["MildLightColor"];

            titleBar.BackgroundColor = titleBar.ButtonBackgroundColor= titleBar.InactiveBackgroundColor = titleBar.ButtonInactiveBackgroundColor = color;
        }

        public void InitializeTheme()
        {
            string theme = _sessionManager.GetApplicationTheme();
            SolidColorBrush mildBackgroundBrush = (SolidColorBrush)Application.Current.Resources["MildBackground"];

            if (theme == null || theme == "" || theme == "dark")
            {
                ((FrameworkElement)Window.Current.Content).RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                ((FrameworkElement)Window.Current.Content).RequestedTheme = ElementTheme.Light;
            }
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = mildBackgroundBrush.Color;
        }

        private void RedirectPage()
        {
            if (_sessionManager.RetriveUserFromStorage() != null)
            {
                Frame.Navigate(typeof(HomePage), _sessionManager.RetriveUserFromStorage());
            }
            else
            {
                Frame.Navigate(typeof(WelcomePage));
            }
        }
    }
}

