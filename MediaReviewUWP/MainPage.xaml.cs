
// The Blank View item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.View.HomePageView;
using MediaReviewUWP.View.WelcomePageView;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MediaReviewUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ISessionManager _sessionManager;
        public MainPage()
        {
            this.InitializeComponent();
            _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();
            InitializeTheme();
            RedirectPage();
        }

        public void InitializeTheme()
        {
            string theme = _sessionManager.GetApplicationTheme();
            if (theme == null || theme == "" || theme == "dark")
            {
                ((FrameworkElement)Window.Current.Content).RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                ((FrameworkElement)Window.Current.Content).RequestedTheme = ElementTheme.Light;
            }
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

