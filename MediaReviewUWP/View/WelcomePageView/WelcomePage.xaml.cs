using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.View.HomePageView;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank View item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaReviewUWP.View.WelcomePageView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WelcomePage : Page
    {
        private ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();
        public WelcomePage()
        {
            this.InitializeComponent();
            UpdateThemeContent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            WelcomePageGrid.Visibility = Visibility.Collapsed;
            LoginUserUC.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            WelcomePageGrid.Visibility = Visibility.Collapsed;
            SignupUserUC.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (SignupUserUC.Visibility == Visibility.Visible)
            {
                SignupUserUC.Visibility = Visibility.Collapsed;
            }
            if (LoginUserUC.Visibility == Visibility.Visible)
            {
                LoginUserUC.Visibility = Visibility.Collapsed;
            }
            BackButton.Visibility = Visibility.Collapsed;
            WelcomePageGrid.Visibility = Visibility.Visible;
        }

        public void NavigateHomePage()
        {
            Frame.Navigate(typeof(HomePage));
        }

        private void ThemeButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var button = sender as Button;
            var currentTheme = ((FrameworkElement)Window.Current.Content).RequestedTheme;
            if (currentTheme == ElementTheme.Dark)
            {
                ((FrameworkElement)Window.Current.Content).RequestedTheme = ElementTheme.Light;
            }
            else
            {
                ((FrameworkElement)Window.Current.Content).RequestedTheme = ElementTheme.Dark;
            }
            UpdateThemeContent();
        }

        private void UpdateThemeContent()
        {
            var currentTheme = ((FrameworkElement)Window.Current.Content).RequestedTheme;
            string theme = "";
            if (currentTheme == ElementTheme.Dark)
            {
                ThemeButton.Content = "☾";
                theme = "dark";
            }
            else
            {
                ThemeButton.Content = "☼";
                theme = "light";
            }
            _sessionManager.SetApplicationTheme(theme);
        }
    }
}
